using System;

namespace LogicParser
{
    internal class LeftParenthesisOperator : Operator
    {
        public override string Name => "(";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            throw new NotImplementedException();
        }
    }

    internal class RightParenthesisOperator : Operator
    {
        public override string Name => ")";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            throw new NotImplementedException();
        }
    }

    internal class AndOperator : Operator
    {
        public override string Name => "&&";

        public override byte Order => 1;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((BoolVariable)left).value && ((BoolVariable)right).value;
        }
    }

    internal class OrOperator : Operator
    {
        public override string Name => "||";

        public override byte Order => 2;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((BoolVariable)left).value || ((BoolVariable)right).value;
        }
    }

    internal class LessOperator : Operator
    {
        public override string Name => "<";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value < ((IntVariable)right).value;
        }
    }

    internal class GreaterOperator : Operator
    {
        public override string Name => ">";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value > ((IntVariable)right).value;
        }
    }

    internal class LessEqualOperator : Operator
    {
        public override string Name => "<=";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value <= ((IntVariable)right).value;
        }
    }

    internal class GreaterEqualOperator : Operator
    {
        public override string Name => ">=";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value >= ((IntVariable)right).value;
        }
    }

    internal class BitfieldOperator : Operator
    {
        public override string Name => "^";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return (((IntVariable)left).value & ((IntVariable)right).value) > 0;
        }
    }
}
