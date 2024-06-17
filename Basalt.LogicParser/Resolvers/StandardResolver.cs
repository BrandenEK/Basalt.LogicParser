using Basalt.LogicParser.Models;

namespace Basalt.LogicParser.Resolvers;

/// <inheritdoc/>
public class StandardResolver(GameInventory inventory) : IResolver
{
    private readonly GameInventory _inventory = inventory;

    /// <inheritdoc/>
    public Variable Resolve(string variable)
    {
        object value = _inventory.GetVariable(variable);

        return value switch
        {
            bool b => new BoolVariable(b),
            int i => new IntVariable(i),
            _ => throw new LogicParserException($"Variable type {value.GetType().Name} is unsupported"),
        };
    }
}
