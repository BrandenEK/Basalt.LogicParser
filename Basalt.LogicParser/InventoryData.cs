using System.Collections.Generic;
using Basalt.LogicParser.Models;

namespace Basalt.LogicParser;

/// <summary>
/// Represents a collection of items used to evaluate logic expressions
/// </summary>
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
            while (operators.Count > 0 && operators.Peek() is not LeftParenthesisOperator)
            {
                tokens.Add(operators.Pop());
            }
            if (operators.Count == 0)
                throw new LogicParserException("Incorrect number of parentheses for expression: " + expression);

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
