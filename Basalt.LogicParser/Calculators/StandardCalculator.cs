using Basalt.LogicParser.Models;
using System.Collections.Generic;

namespace Basalt.LogicParser.Calculators;

/// <inheritdoc/>
public class StandardCalculator : ICalculator
{
    /// <inheritdoc/>
    public bool Calculate(string expression, IEnumerable<Token> tokens)
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
}
