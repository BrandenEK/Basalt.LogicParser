using Basalt.LogicParser.Models;
using System.Collections.Generic;

namespace Basalt.LogicParser.Calculators;

/// <summary>
/// Calculates the result of the tokens by evaluating them as a postfix stack
/// </summary>
public class PostfixCalculator : ICalculator
{
    /// <inheritdoc/>
    public bool Calculate(IEnumerable<Token> tokens)
    {
        var stack = new Stack<Variable>();

        // Process each token in the list and add its result to the stack
        foreach (var token in tokens)
        {
            ProcessToken(token, stack);
        }

        // Ensure that the stack is fully completed and a bool is on top
        if (stack.Count != 1 || stack.Peek() is not BoolVariable)
            throw new LogicParserException("Invalid order of operations");

        return ((BoolVariable)stack.Pop()).Value;
    }

    /// <summary>
    /// Handles the logic of the surrounding tokens and pushes the result to the stack
    /// </summary>
    private void ProcessToken(Token token, Stack<Variable> stack)
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
                throw new LogicParserException("Invalid order of operations");

            Variable right = stack.Pop(), left = stack.Pop();
            bool result = op.Evaluate(left, right);
            stack.Push(new BoolVariable(result));
            return;
        }
    }
}
