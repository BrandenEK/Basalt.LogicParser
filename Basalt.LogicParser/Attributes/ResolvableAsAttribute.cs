using System;

namespace Basalt.LogicParser.Attributes;

/// <summary>
/// Returns this property's value when resolving its name
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
public class ResolvableAsAttribute(string name) : Attribute
{
    internal string Name { get; } = name;
}
