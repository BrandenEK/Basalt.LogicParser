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
    private readonly Dictionary<string, IEnumerable<PropertyInfo>> _collectableProperties;

    /// <summary> </summary>
    public ReflectionCollector(object info)
    {
        _info = info;
        _collectableProperties = info.GetType()
            .GetProperties()
            .Where(IsValidCollectable)
            .SelectMany(p => GetAttribute(p).Items, (p, item) => new KeyValuePair<string, PropertyInfo>(item, p))
            .GroupBy(match => match.Key)
            .ToDictionary(group => group.Key, group => group.Select(match => match.Value));
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
        if (!_collectableProperties.TryGetValue(item, out IEnumerable<PropertyInfo>? properties))
            return;

        foreach (var property in properties)
        {
            UpdateProperty(property, isAddition);
        }
    }

    private void UpdateProperty(PropertyInfo property, bool isAddition)
    {
        var type = Type.GetTypeCode(property.PropertyType);
        switch (type)
        {
            case TypeCode.Boolean:
                property.SetValue(_info, isAddition, null);
                break;
            case TypeCode.Int32:
                property.SetValue(_info, (int)(property.GetValue(_info, null) ?? 0) + (isAddition ? 1 : -1), null);
                break;
            default:
                throw new LogicParserException($"Variable type {type} is unsupported");
        }
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

    private CollectableAsAttribute GetAttribute(PropertyInfo property)
    {
        return (CollectableAsAttribute)property.GetCustomAttributes(typeof(CollectableAsAttribute), false)[0];
    }
}
