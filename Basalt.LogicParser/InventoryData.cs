﻿using Basalt.LogicParser.Calculators;
using Basalt.LogicParser.Formatters;
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
        if (string.IsNullOrEmpty(expression))
            return true;

        IFormatter formatter = new StandardFormatter();
        IResolver resolver = new StandardResolver(this);
        IParser parser = new StandardParser(resolver);
        ICalculator calculator = new StandardCalculator();

        LogicParserException.CurrentExpression = expression;
        return calculator.Calculate(parser.Parse(formatter.Format(expression)));
    }

    /// <summary>
    /// Get an object representing the value of the specified variable from the child class
    /// </summary>
    protected internal abstract object GetVariable(string variable);
}
