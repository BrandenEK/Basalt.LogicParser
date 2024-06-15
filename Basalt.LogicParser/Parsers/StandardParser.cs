using Basalt.LogicParser.Models;
using System.Collections.Generic;

namespace Basalt.LogicParser.Parsers;

public class StandardParser : IParser
{
    /// <summary>
    /// Converts the expression into a list of tokens in postfix notation
    /// </summary>
    private IEnumerable<Token> CalculateTokens(string expression)
    {
        var tokens = new List<Token>();
        var operators = new Stack<Operator>();

        // Calculate each token in the split string and add it to the list of tokens
        foreach (string token in expression.Split(' '))
        {
            CalculateToken(expression, token, tokens, operators);
        }

        // Add remaining operators
        while (operators.Count > 0)
        {
            if (operators.Peek() is LeftParenthesisOperator)
                throw new LogicParserException("Incorrect number of parentheses for expression: " + expression);

            tokens.Add(operators.Pop());
        }

        return tokens;
    }

    /// <summary>
    /// Processes the token as a string and converts it into either a variable or an operator
    /// </summary>
    private void CalculateToken(string expression, string token, List<Token> tokens, Stack<Operator> operators)
    {
        // Extra empty space is not allowed
        if (token == string.Empty)
        {
            throw new LogicParserException("Extra space for expression: " + expression);
        }

        // Regular integer number
        if (int.TryParse(token, out int num))
        {
            tokens.Add(new IntVariable(num));
            return;
        }

        // Registered operator
        if (_allOperators.ContainsKey(token))
        {
            CalculateOperator(expression, token, tokens, operators);
            return;
        }

        // If none of these, assume a game variable
        tokens.Add(GetVariableValue(token));
    }

    /// <summary>
    /// Adds padding around parentheses, if not already present
    /// </summary>
    private string AddPaddingAroundTokens(string expression)
    {
        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i] == '(' || expression[i] == ')')
            {
                if (i > 0 && expression[i - 1] != ' ')
                {
#if NET35
                    expression = expression.Substring(0, i) + ' ' + expression.Substring(i);
#elif NET6_0_OR_GREATER
                    expression = string.Concat(expression.AsSpan(0, i), " ", expression.AsSpan(i));
#endif
                    i++;
                }
                if (i < expression.Length - 1 && expression[i + 1] != ' ')
                {
#if NET35
                    expression = expression.Substring(0, i + 1) + ' ' + expression.Substring(i + 1);
#elif NET6_0_OR_GREATER
                    expression = string.Concat(expression.AsSpan(0, i + 1), " ", expression.AsSpan(i + 1));
#endif
                    i++;
                }
            }
        }

        return expression;
    }
}
