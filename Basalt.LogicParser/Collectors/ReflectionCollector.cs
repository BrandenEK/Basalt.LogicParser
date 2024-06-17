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
    private readonly Dictionary<CollectableAsAttribute, PropertyInfo> _collectableProperties;

    /// <summary> </summary>
    public ReflectionCollector(object info)
    {
        _collectableProperties = info.GetType().GetProperties()
            .Where(p => p.CanRead && p.CanWrite && p.IsDefined(typeof(CollectableAsAttribute), false))
            .ToDictionary(p => (CollectableAsAttribute)p.GetCustomAttributes(typeof(CollectableAsAttribute), false)[0], p => p);
    
        foreach (var kvp in _collectableProperties)
        {
            Console.WriteLine($"{kvp.Value.Name}: {kvp.Key.Item}");
        }
    }

    /// <inheritdoc/>
    public void Add(string item)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void Remove(string item)
    {
        throw new System.NotImplementedException();
    }
}
