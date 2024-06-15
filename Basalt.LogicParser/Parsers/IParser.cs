using Basalt.LogicParser.Models;
using System.Collections.Generic;

namespace Basalt.LogicParser.Parsers;

/// <summary>
/// Parses a logic expression into a collection of tokens
/// </summary>
public interface IParser
{
    /// <summary>
    /// Parses a logic expression into a collection of tokens
    /// </summary>
    public IEnumerable<Token> Parse(string expression);
}
