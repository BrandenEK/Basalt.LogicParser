using Basalt.LogicParser.Calculators;
using Basalt.LogicParser.Formatters;
using Basalt.LogicParser.Parsers;
using Basalt.LogicParser.Resolvers;

namespace Basalt.LogicParser;

/// <summary>
/// Represents a collection of items used to evaluate logic expressions
/// </summary>
public class GameInventory(ICalculator calculator, IFormatter formatter, IParser parser, IResolver resolver)
{
    private readonly ICalculator _calculator = calculator;
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
}
