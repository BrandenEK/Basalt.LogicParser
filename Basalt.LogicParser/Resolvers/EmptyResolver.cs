using Basalt.LogicParser.Models;

namespace Basalt.LogicParser.Resolvers;

/// <inheritdoc/>
public class EmptyResolver : IResolver
{
    /// <inheritdoc/>
    public Variable Resolve(string variable)
    {
        throw new LogicParserException("IResolver not defined");
    }
}
