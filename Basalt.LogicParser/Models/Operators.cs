
namespace Basalt.LogicParser.Models;

/// <summary>
/// Represents an operator in a logic expression
/// </summary>
public abstract class Operator(byte order) : Token
{
    /// <summary>
    /// The order in which this operator will be evaluated
    /// </summary>
    public byte Order { get; } = order;

    /// <summary>
    /// Evaluates two variables and returns the result
    /// </summary>
    public abstract bool Evaluate(Variable left, Variable right);
}

/// <inheritdoc/>
public class LeftParenthesisOperator() : Operator(0)
{
    /// <inheritdoc/>
    public override bool Evaluate(Variable left, Variable right)
    {
        throw new LogicParserException("Can not evaluate a parenthesis token!");
    }
}

/// <inheritdoc/>
public class RightParenthesisOperator() : Operator(0)
{
    /// <inheritdoc/>
    public override bool Evaluate(Variable left, Variable right)
    {
        throw new LogicParserException("Can not evaluate a parenthesis token!");
    }
}

/// <inheritdoc/>
public class AndOperator() : Operator(1)
{
    /// <inheritdoc/>
    public override bool Evaluate(Variable left, Variable right)
    {
        return ((BoolVariable)left).Value && ((BoolVariable)right).Value;
    }
}

/// <inheritdoc/>
public class OrOperator() : Operator(2)
{
    /// <inheritdoc/>
    public override bool Evaluate(Variable left, Variable right)
    {
        return ((BoolVariable)left).Value || ((BoolVariable)right).Value;
    }
}

/// <inheritdoc/>
public class LessOperator() : Operator(0)
{
    /// <inheritdoc/>
    public override bool Evaluate(Variable left, Variable right)
    {
        return ((IntVariable)left).Value < ((IntVariable)right).Value;
    }
}

/// <inheritdoc/>
public class GreaterOperator() : Operator(0)
{
    /// <inheritdoc/>
    public override bool Evaluate(Variable left, Variable right)
    {
        return ((IntVariable)left).Value > ((IntVariable)right).Value;
    }
}

/// <inheritdoc/>
public class LessEqualOperator() : Operator(0)
{
    /// <inheritdoc/>
    public override bool Evaluate(Variable left, Variable right)
    {
        return ((IntVariable)left).Value <= ((IntVariable)right).Value;
    }
}

/// <inheritdoc/>
public class GreaterEqualOperator() : Operator(0)
{
    /// <inheritdoc/>
    public override bool Evaluate(Variable left, Variable right)
    {
        return ((IntVariable)left).Value >= ((IntVariable)right).Value;
    }
}
