﻿using System;
using System.Reflection;

namespace Basalt.LogicParser.Attributes;

/// <summary>
/// Returns this property's value when resolving its name
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
public class ResolvableAsAttribute(params string[] names) : Attribute
{
    internal string[] Names { get; } = names;
}

internal class NamePropertyMatch(string name, PropertyInfo property)
{
    public string Name { get; } = name;
    public PropertyInfo Property { get; } = property;
}