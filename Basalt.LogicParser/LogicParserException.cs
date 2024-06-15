using System;

namespace Basalt.LogicParser;

/// <summary>
/// An error that occurs during parsing
/// </summary>
public class LogicParserException(string message) : Exception(message)
{
}
