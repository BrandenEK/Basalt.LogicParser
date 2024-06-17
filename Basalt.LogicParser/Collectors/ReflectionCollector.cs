using Basalt.LogicParser.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Basalt.LogicParser.Collectors;

/// <summary>
/// Adds or removes items based on collectable attributes on the info object
/// </summary>
public class ReflectionCollector : ICollector
{
    private readonly object _info;
    private readonly Dictionary<CollectableAsAttribute, PropertyInfo> _collectableProperties;

    /// <summary> </summary>
    public ReflectionCollector(object info)
    {
        _info = info;
        _collectableProperties = info.GetType().GetProperties().Where(IsValidCollectable)
            .ToDictionary(p => (CollectableAsAttribute)p.GetCustomAttributes(typeof(CollectableAsAttribute), false)[0], p => p);
    }

    /// <inheritdoc/>
    public void Add(string item)
    {
        UpdateProperties(item, true);
    }

    /// <inheritdoc/>
    public void Remove(string item)
    {
        UpdateProperties(item, false);
    }

    private void UpdateProperties(string item, bool isAddition)
    {
        foreach (var kvp in _collectableProperties.Where(kvp => kvp.Key.Item == item))
        {
            UpdateProperty(kvp.Value, isAddition);
        }
    }

    private void UpdateProperty(PropertyInfo property, bool isAddition)
    {
        switch (Type.GetTypeCode(property.PropertyType))
        {
            case TypeCode.Boolean:
                UpdateBooleanProperty(property, isAddition);
                break;
            case TypeCode.Int32:
                UpdateIntegerProperty(property, isAddition);
                break;
        }
    }

    private void UpdateBooleanProperty(PropertyInfo property, bool isAddition)
    {
        property.SetValue(_info, isAddition, null);
    }

    private void UpdateIntegerProperty(PropertyInfo property, bool isAddition)
    {
        property.SetValue(_info, (int)(property.GetValue(_info, null) ?? 0) + (isAddition ? 1 : -1), null);
    }

    private bool IsValidCollectable(PropertyInfo property)
    {
        // Ensure there is a collectable atribute
        if (!property.IsDefined(typeof(CollectableAsAttribute), false))
            return false;

        // Ensure there is getter and setter
        if (!property.CanRead || !property.CanWrite)
            return false;

        // Ensure it is valid type
        if (property.PropertyType != typeof(bool) && property.PropertyType != typeof(int))
            return false;

        return true;
    }
}
