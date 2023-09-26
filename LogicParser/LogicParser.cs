﻿using System.Collections.Generic;

namespace LogicParser
{
    public class LogicParser
    {
        private static readonly Dictionary<string, Operator> _allOperators;

        // Fake testing method
        public static bool Evaluate(InventoryData inventory, string expression)
        {
            return ((BoolVariable)inventory.GetVariableValue(expression)).value;
        }

        static LogicParser()
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