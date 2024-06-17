using System;

namespace Basalt.LogicParser.Attributes;

/// <summary>
/// Modifies this property when the specified item is collected
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class CollectableAsAttribute(params string[] items) : Attribute
{
    internal string[] Items { get; } = items;
}
