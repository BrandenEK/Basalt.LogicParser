using System;

namespace Basalt.LogicParser;

/// <summary>
/// An error that occurs during parsing
/// </summary>
public class LogicParserException : Exception
{
    /// <summary>
    /// Creates a new logic exception
    /// </summary>
    public LogicParserException(string message) : base(message) { }
}
