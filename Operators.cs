using System;

namespace LogicParser
{
    public class LeftParenthesisOperator : Operator
    {
        public override string Name => "(";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            throw new LogicParserException("Can not evaluate a prenthesis token!");
        }
    }

    public class RightParenthesisOperator : Operator
    {
        public override string Name => ")";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            throw new LogicParserException("Can not evaluate a prenthesis token!");
        }
    }

    public class AndOperator : Operator
    {
        public override string Name => "&&";

        public override byte Order => 1;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((BoolVariable)left).value && ((BoolVariable)right).value;
        }
    }

    public class OrOperator : Operator
    {
        public override string Name => "||";

        public override byte Order => 2;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((BoolVariable)left).value || ((BoolVariable)right).value;
        }
    }

    public class LessOperator : Operator
    {
        public override string Name => "<";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value < ((IntVariable)right).value;
        }
    }

    public class GreaterOperator : Operator
    {
        public override string Name => ">";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value > ((IntVariable)right).value;
        }
    }

    public class LessEqualOperator : Operator
    {
        public override string Name => "<=";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value <= ((IntVariable)right).value;
        }
    }

    public class GreaterEqualOperator : Operator
    {
        public override string Name => ">=";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return ((IntVariable)left).value >= ((IntVariable)right).value;
        }
    }

    public class BitfieldOperator : Operator
    {
        public override string Name => "^";

        public override byte Order => 0;

        public override bool Evaluate(Variable left, Variable right)
        {
            return (((IntVariable)left).value & ((IntVariable)right).value) > 0;
        }
    }
}
