using Basalt.LogicParser.Models;

namespace Basalt.LogicParser.Resolvers;

/// <summary>
/// Resolves a string into a variable object
/// </summary>
public interface IResolver
{
    /// <summary>
    /// Resolves a string into a variable object
    /// </summary>
    public Variable Resolve(string variable);
}
