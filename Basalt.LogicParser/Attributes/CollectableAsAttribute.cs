using System;

namespace Basalt.LogicParser.Attributes;

/// <summary>
/// Modifies this property when the specified item is collected
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
public class CollectableAsAttribute(string item) : Attribute
{
    internal string Item { get; } = item;
}
