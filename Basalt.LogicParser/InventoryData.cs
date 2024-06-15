using System;
using System.Collections.Generic;
using Basalt.LogicParser.Models;

namespace Basalt.LogicParser;

public abstract class InventoryData
{
    /// <summary>
    /// Converts the string expression to a logical statement and evaluates it
    /// </summary>
    public bool Evaluate(string expression)
    {
        // If nothing, then its true
        if (expression == null || expression == string.Empty)
        {
            return true;
        }

        expression = AddPaddingAroundTokens(expression);
        var tokens = CalculateTokens(expression);

        return ProcessTokens(expression, tokens);
    }

    /// <summary>
    /// Evaluate the tokens in postfix notation as a stack until the final result is left
    /// </summary>
    private bool ProcessTokens(string expression, IEnumerable<Token> tokens)
    {
        var stack = new Stack<Variable>();

        // Process each token in the list and add its result to the stack
        foreach (var token in tokens)
        {
            ProcessToken(expression, token, stack);
        }

        // Ensure that the stack is fully completed
        if (stack.Count != 1)
            throw new LogicParserException("Order of operations was incorrect for expression: " + expression);

        return (stack.Pop() as BoolVariable).Value;
    }

    /// <summary>
    /// Handles the logic of the surrounding tokens and pushes the result to the stack
    /// </summary>
    private void ProcessToken(string expression, Token token, Stack<Variable> stack)
    {
        // Variable token
        if (token is Variable var)
        {
            stack.Push(var);
            return;
        }

        // Operator token
        if (token is Operator op)
        {
            if (stack.Count < 2)
                throw new LogicParserException("Order of operations was incorrect for expression: " + expression);

            Variable right = stack.Pop(), left = stack.Pop();
            bool result = op.Evaluate(left, right);
            stack.Push(new BoolVariable(result));
            return;
        }
    }

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
    /// When parsing the expression and encountered an operator, perform logic to move tokens around
    /// </summary>
    private void CalculateOperator(string expression, string token, List<Token> tokens, Stack<Operator> operators)
    {
        Operator op = _allOperators[token];

        // Left parenthesis
        if (op is LeftParenthesisOperator)
        {
            operators.Push(op);
            return;
        }

        // Right parenthesis
        if (op is RightParenthesisOperator)
        {
            while (operators.Count > 0 && !(operators.Peek() is LeftParenthesisOperator))
            {
                tokens.Add(operators.Pop());
            }
            if (operators.Count == 0)
                throw new LogicParserException("Incorrect number of parentheses for expression: " + expression);

            operators.Pop();
            return;
        }

        // Regular operator
        while (operators.Count > 0 && !(operators.Peek() is LeftParenthesisOperator) && operators.Peek().order <= op.order)
        {
            tokens.Add(operators.Pop());
        }
        operators.Push(op);
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

    /// <summary>
    /// Get an object representing the value of the specified variable from the child class
    /// </summary>
    protected abstract object GetVariable(string variable);

    /// <summary>
    /// Convert the variable based on its type into the wrapper class
    /// </summary>
    private Variable GetVariableValue(string variable)
    {
        object value = GetVariable(variable);
        switch (value)
        {
            case bool b: return new BoolVariable(b);
            case int i: return new IntVariable(i);
            default: throw new LogicParserException($"Unsupported variable type: {value.GetType().Name}");
        }
    }

    /// <summary>
    /// Create and initialize the dictionary of valid operators
    /// </summary>
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

    private static readonly Dictionary<string, Operator> _allOperators;
}
