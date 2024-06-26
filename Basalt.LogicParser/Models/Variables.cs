﻿
namespace Basalt.LogicParser.Models;

/// <summary>
/// Represents a variable in a logic expression
/// </summary>
public abstract class Variable : Token { }

/// <inheritdoc/>
public class IntVariable(int value) : Variable
{
    /// <summary> The value of this variable </summary>
    public int Value { get; } = value;
}

/// <inheritdoc/>
public class BoolVariable(bool value) : Variable
{
    /// <summary> The value of this variable </summary>
    public bool Value { get; } = value;
}
