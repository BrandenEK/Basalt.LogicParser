using System;

namespace Basalt.LogicParser;

/// <summary>
/// An error that occurs during parsing
/// </summary>
public class LogicParserException(string mesage) : Exception($"{mesage} for expression: {CurrentExpression}")
{
    /// <summary>
    /// The current expression being evaluated
    /// </summary>
    public static string CurrentExpression { get; set; }
}
