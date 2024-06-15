using Basalt.LogicParser.Models;
using System.Collections.Generic;

namespace Basalt.LogicParser.Calculators;

/// <summary>
/// Calculates a boolean result from a collection of tokens
/// </summary>
public interface ICalculator
{
    /// <summary>
    /// Calculates a boolean result from a collection of tokens
    /// </summary>
    public bool Calculate(IEnumerable<Token> tokens);
}
