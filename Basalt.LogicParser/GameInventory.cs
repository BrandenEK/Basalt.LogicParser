using Basalt.LogicParser.Calculators;
using Basalt.LogicParser.Collectors;
using Basalt.LogicParser.Formatters;
using Basalt.LogicParser.Parsers;
using Basalt.LogicParser.Resolvers;

namespace Basalt.LogicParser;

/// <summary>
/// Represents a collection of items used to evaluate logic expressions
/// </summary>
public class GameInventory(ICalculator calculator, ICollector collector, IFormatter formatter, IParser parser, IResolver resolver)
{
    private readonly ICalculator _calculator = calculator;
    private readonly ICollector _collector = collector;
    private readonly IFormatter _formatter = formatter;
    private readonly IParser _parser = parser;
    private readonly IResolver _resolver = resolver;

    /// <summary>
    /// Converts the string expression to a logical statement and evaluates it
    /// </summary>
    public bool Evaluate(string expression)
    {
        LogicParserException.CurrentExpression = expression;

        return string.IsNullOrEmpty(expression) || _calculator.Calculate(_parser.Parse(_formatter.Format(expression), _resolver));
    }

    /// <summary>
    /// Adds the item to the inventory
    /// </summary>
    public void Add(string item)
    {
        _collector.Add(item);
    }

    /// <summary>
    /// Removes the item from the inventory
    /// </summary>
    public void Remove(string item)
    {
        _collector.Remove(item);
    }
}
