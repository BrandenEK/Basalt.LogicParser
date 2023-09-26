
namespace LogicParser
{
    internal class LeftParenthesisOperator : Operator
    {
        public LeftParenthesisOperator() : base("(", 0) { }

        public override bool Evaluate(Variable left, Variable right)
        {
            throw new LogicParserException("Can not evaluate a parenthesis token!");
        }
    }

    internal class RightParenthesisOperator : Operator
    {
        public RightParenthesisOperator() : base(")", 0) { }

        public override bool Evaluate(Variable left, Variable right)
        {
            throw new LogicParserException("Can not evaluate a parenthesis token!");
        }
    }

    internal class AndOperator : Operator
    {
        public AndOperator() : base("+", 1) { }

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((BoolVariable)left).value && ((BoolVariable)right).value;
        }
    }

    internal class OrOperator : Operator
    {
        public OrOperator() : base("|", 2) { }

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((BoolVariable)left).value || ((BoolVariable)right).value;
        }
    }

    internal class LessOperator : Operator
    {
        public LessOperator() : base("<", 0) { }

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value < ((IntVariable)right).value;
        }
    }

    internal class GreaterOperator : Operator
    {
        public GreaterOperator() : base(">", 0) { }

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value > ((IntVariable)right).value;
        }
    }

    internal class LessEqualOperator : Operator
    {
        public LessEqualOperator() : base("<=", 0) { }

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value <= ((IntVariable)right).value;
        }
    }

    internal class GreaterEqualOperator : Operator
    {
        public GreaterEqualOperator() : base(">=", 0) { }

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value >= ((IntVariable)right).value;
        }
    }
}
