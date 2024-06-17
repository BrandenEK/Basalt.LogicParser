using Basalt.LogicParser.Attributes;
using Basalt.LogicParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Basalt.LogicParser.Resolvers;

/// <summary>
/// Locates variable values based on resolvable attributes on the info object
/// </summary>
public class ReflectionResolver : IResolver
{
    private readonly object _info;
    private readonly Dictionary<string, PropertyInfo> _resolvableProperties;

    /// <summary> </summary>
    public ReflectionResolver(object info)
    {
        _info = info;
        _resolvableProperties = info.GetType()
            .GetProperties()
            .Where(IsValidResolvable)
            .SelectMany(p => GetAttribute(p).Names, (p, name) => new NamePropertyMatch(name, p))
            .ToDictionary(match => match.Name, match => match.Property);
    }

    /// <inheritdoc/>
    public Variable Resolve(string variable)
    {
        if (!_resolvableProperties.TryGetValue(variable, out PropertyInfo? property))
            throw new LogicParserException($"\"{variable}\" could not be resolved");

        var type = Type.GetTypeCode(property.PropertyType);
        return type switch
        {
            TypeCode.Boolean => new BoolVariable((bool)(property.GetValue(_info, null) ?? false)),
            TypeCode.Int32 => new IntVariable((int)(property.GetValue(_info, null) ?? 0)),
            _ => throw new LogicParserException($"Variable type {type} is unsupported")
        };
    }

    private bool IsValidResolvable(PropertyInfo property)
    {
        // Ensure there is a resolvable atribute
        if (!property.IsDefined(typeof(ResolvableAsAttribute), false))
            return false;

        // Ensure there is getter
        if (!property.CanRead)
            return false;

        // Ensure it is valid type
        if (property.PropertyType != typeof(bool) && property.PropertyType != typeof(int))
            return false;

        return true;
    }

    private ResolvableAsAttribute GetAttribute(PropertyInfo property)
    {
        return (ResolvableAsAttribute)property.GetCustomAttributes(typeof(ResolvableAsAttribute), false)[0];
    }
}
