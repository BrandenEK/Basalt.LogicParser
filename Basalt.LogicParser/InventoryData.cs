﻿using System.Collections.Generic;
using Basalt.LogicParser.Models;
using Basalt.LogicParser.Parsers;
using Basalt.LogicParser.Resolvers;

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

        IParser parser = new StandardParser(new StandardResolver(this));

        var tokens = parser.Parse(expression);

        return ProcessTokens(expression, tokens);
    }

    /// <summary>
    /// Get an object representing the value of the specified variable from the child class
    /// </summary>
    protected internal abstract object GetVariable(string variable);

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
}
