using Basalt.LogicParser.Models;

namespace Basalt.LogicParser.Resolvers;

/// <summary>
/// Resolves the variable by calling an abstract method
/// </summary>
public abstract class LegacyResolver : IResolver
{
    /// <summary>
    /// Get an object representing the value of the specified variable from the child class
    /// </summary>
    protected abstract object GetVariable(string variable);

    /// <inheritdoc/>
    public Variable Resolve(string variable)
    {
        object value = GetVariable(variable);

        return value switch
        {
            bool b => new BoolVariable(b),
            int i => new IntVariable(i),
            _ => throw new LogicParserException($"Variable type {value.GetType().Name} is unsupported"),
        };
    }
}
