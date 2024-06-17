using System;

namespace Basalt.LogicParser.Attributes;

/// <summary>
/// Returns this property's value when resolving its name
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class ResolvableAsAttribute(params string[] names) : Attribute
{
    internal string[] Names { get; } = names;
}
