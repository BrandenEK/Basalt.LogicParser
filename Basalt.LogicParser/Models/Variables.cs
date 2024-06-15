
namespace Basalt.LogicParser.Models;

/// <summary>
/// Represents a variable in a logic expression
/// </summary>
public abstract class Variable : Token { }

/// <summary>
/// An integer variable
/// </summary>
public class IntVariable(int value) : Variable
{
    /// <summary> The value of this variable </summary>
    public int Value { get; } = value;
}

/// <summary>
/// A boolean variable
/// </summary>
public class BoolVariable(bool value) : Variable
{
    /// <summary> The value of this variable </summary>
    public bool Value { get; } = value;
}
