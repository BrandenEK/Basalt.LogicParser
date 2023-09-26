
using System.Collections.Generic;

namespace LogicParser
{
    public abstract class InventoryData
    {

        public bool Evaluate(string expression)
        {

        }

        // Variable handling

        protected abstract object GetVariable(string variable);

        private Variable GetVariableValue(string variable)
        {
            object value = GetVariable(variable);
            switch (value)
            {
                case bool b:
                    return new BoolVariable(b);
                case int i:
                    return new IntVariable(i);
                default:
                    throw new LogicParserException($"Unsupported variable type: {value.GetType().Name}");
            }
        }

        // Operator storage

        private static readonly Dictionary<string, Operator> _allOperators;

        static InventoryData()
        {
            var leftParenthesis = new LeftParenthesisOperator();
            var rightParenthesis = new RightParenthesisOperator();
            var and = new AndOperator();
            var or = new OrOperator();

            _allOperators = new Dictionary<string, Operator>()
            {
                { "(", leftParenthesis },
                { ")", rightParenthesis },
                { "[", leftParenthesis },
                { "]", rightParenthesis },
                { "&&", and },
                { "+", and },
                { "||", or },
                { "|", or },
                { "<", new LessOperator() },
                { ">", new GreaterOperator() },
                { "<=", new LessEqualOperator() },
                { ">=", new GreaterEqualOperator() },
            };
        }
    }
}
