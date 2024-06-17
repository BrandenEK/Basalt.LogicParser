using Basalt.LogicParser.Models;
using Basalt.LogicParser.Resolvers;
using System.Collections.Generic;

namespace Basalt.LogicParser.Parsers;

/// <summary>
/// Parses each whitespace-separated token and orders them using postfix notation
/// </summary>
public class PostfixParser : IParser
{
    /// <inheritdoc/>
    public IEnumerable<Token> Parse(string expression, IResolver resolver)
    {
        var tokens = new List<Token>();
        var operators = new Stack<Operator>();

        // Calculate each token in the split string and add it to the list of tokens
        foreach (string token in expression.Split(' '))
        {
            CalculateToken(token, resolver, tokens, operators);
        }

        // Add remaining operators
        while (operators.Count > 0)
        {
            if (operators.Peek() is LeftParenthesisOperator)
                throw new LogicParserException("Incorrect number of parentheses");

            tokens.Add(operators.Pop());
        }

        return tokens;
    }

    /// <summary>
    /// Processes the token as a string and converts it into either a variable or an operator
    /// </summary>
    private void CalculateToken(string token, IResolver resolver, List<Token> tokens, Stack<Operator> operators)
    {
        // Extra empty space is not allowed
        if (token == string.Empty)
        {
            throw new LogicParserException("Extra whitespace");
        }

        // Regular integer number
        if (int.TryParse(token, out int num))
        {
            tokens.Add(new IntVariable(num));
            return;
        }

        // Registered operator
        if (Operator.All.ContainsKey(token))
        {
            CalculateOperator(token, tokens, operators);
            return;
        }

        // If none of these, assume a game variable
        tokens.Add(resolver.Resolve(token));
    }

    /// <summary>
    /// When parsing the expression and encountered an operator, perform logic to move tokens around
    /// </summary>
    private void CalculateOperator(string token, List<Token> tokens, Stack<Operator> operators)
    {
        Operator op = Operator.All[token];

        // Left parenthesis
        if (op is LeftParenthesisOperator)
        {
            operators.Push(op);
            return;
        }

        // Right parenthesis
        if (op is RightParenthesisOperator)
        {
            while (operators.Count > 0 && operators.Peek() is not LeftParenthesisOperator)
            {
                tokens.Add(operators.Pop());
            }
            if (operators.Count == 0)
                throw new LogicParserException("Incorrect number of parentheses");

            operators.Pop();
            return;
        }

        // Regular operator
        while (operators.Count > 0 && operators.Peek() is not LeftParenthesisOperator && operators.Peek().Order <= op.Order)
        {
            tokens.Add(operators.Pop());
        }
        operators.Push(op);
    }
}
