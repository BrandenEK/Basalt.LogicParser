using System;
using System.Collections.Generic;

namespace LogicParser
{
    public static class Parser
    {
        public static bool EvaluateExpression(string expression, InventoryData inventory)
        {
            // Add padding around parenthesis if not already present
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '(' || expression[i] == ')')
                {
                    if (i > 0 && expression[i - 1] != ' ')
                    {
                        expression = expression.Substring(0, i) + ' ' + expression.Substring(i);
                        i++;
                    }
                    if (i < expression.Length - 1 && expression[i + 1] != ' ')
                    {
                        expression = expression.Substring(0, i + 1) + ' ' + expression.Substring(i + 1);
                        i++;
                    }
                }
            }

            // Convert the string expression into a list of tokens
            string[] expressionSplit = expression.Split(' ');
            List<Token> tokens = new List<Token>();
            Stack<Operator> operators = new Stack<Operator>();

            foreach (string token in expressionSplit)
            {
                if (token == string.Empty) // Extra space
                {
                    throw new LogicParserException("Extra space for expression: " + expression);
                }
                else if (int.TryParse(token, out int num)) // Decimal number
                {
                    tokens.Add(new IntVariable(num));
                }
                else if (token.Length > 2 && token.Substring(0, 2) == "0x") // Hex number
                {
                    int hex = Convert.ToInt32(token.Substring(2), 16);
                    tokens.Add(new IntVariable(hex));
                }
                else if (allOperators.ContainsKey(token)) // Operator
                {
                    Operator op = allOperators[token];

                    if (op.Name == "(") // Left parenthesis
                    {
                        operators.Push(op);
                    }
                    else if (op.Name == ")") // Right parenthesis
                    {
                        while (operators.Count > 0 && operators.Peek().Name != "(")
                        {
                            tokens.Add(operators.Pop());
                        }
                        if (operators.Count == 0)
                            throw new LogicParserException("Incorrect number of parentheses for expression: " + expression);

                        operators.Pop();
                    }
                    else // Regular operator
                    {
                        while (operators.Count > 0 && operators.Peek().Name != "(" && operators.Peek().Order <= op.Order)
                        {
                            tokens.Add(operators.Pop());
                        }
                        operators.Push(op);
                    }
                }
                else // Assume a game variable
                {
                    tokens.Add(inventory.GetVariable(token));
                }
            }

            // Add remaining operators
            while (operators.Count > 0)
            {
                if (operators.Peek().Name == "(")
                    throw new LogicParserException("Incorrect number of parentheses for expression: " + expression);

                tokens.Add(operators.Pop());
            }

            // Evaluate the tokens in postfix notation as a stack until the final result is left
            Stack<Variable> stack = new Stack<Variable>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is Variable var)
                {
                    stack.Push(var);
                }
                else if (tokens[i] is Operator op)
                {
                    if (stack.Count < 2)
                        throw new LogicParserException("Order of operations was incorrect for expression: " + expression);

                    Variable right = stack.Pop(), left = stack.Pop();
                    bool result = op.Evaluate(left, right);
                    stack.Push(new BoolVariable(result));
                }
            }

            if (stack.Count != 1)
                throw new LogicParserException("Order of operations was incorrect for expression: " + expression);

            return (stack.Pop() as BoolVariable).value;
        }

        public static void RegisterOperator(Operator op)
        {
            if (!allOperators.ContainsKey(op.Name))
                allOperators.Add(op.Name, op);
        }

        private static Dictionary<string, Operator> allOperators = new Dictionary<string, Operator>()
        {
            { "(", new LeftParenthesisOperator() },
            { ")", new RightParenthesisOperator() },
            { "&&", new AndOperator() },
            { "||", new OrOperator() },
            { "<", new LessOperator() },
            { ">", new GreaterOperator() },
            { "<=", new LessEqualOperator() },
            { ">=", new GreaterEqualOperator() },
            { "^", new BitfieldOperator() },
        };
    }
}
