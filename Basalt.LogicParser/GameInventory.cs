using Basalt.LogicParser.Calculators;
using Basalt.LogicParser.Formatters;
using Basalt.LogicParser.Parsers;
using Basalt.LogicParser.Resolvers;

namespace Basalt.LogicParser;

/// <summary>
/// Represents a collection of items used to evaluate logic expressions
/// </summary>
public abstract class GameInventory
{
    private readonly IFormatter _formatter;
    private readonly IResolver _resolver;
    private readonly IParser _parser;
    private readonly ICalculator _calculator;

    /// <summary>
    /// Initializes a new inventory with standard functionality
    /// </summary>
    public GameInventory()
    {
        _formatter = new StandardFormatter();
        _resolver = new StandardResolver(this);
        _parser = new StandardParser(_resolver);
        _calculator = new StandardCalculator();
    }

    /// <summary>
    /// Initializes a new inventory with custom functionality
    /// </summary>
    public GameInventory(IFormatter formatter, IResolver resolver, IParser parser, ICalculator calculator)
    {
        _formatter = formatter;
        _resolver = resolver;
        _parser = parser;
        _calculator = calculator;
    }

    /// <summary>
    /// Converts the string expression to a logical statement and evaluates it
    /// </summary>
    public bool Evaluate(string expression)
    {
        LogicParserException.CurrentExpression = expression;

        return string.IsNullOrEmpty(expression) || _calculator.Calculate(_parser.Parse(_formatter.Format(expression)));
    }

    /// <summary>
    /// Get an object representing the value of the specified variable from the child class
    /// </summary>
    protected internal abstract object GetVariable(string variable);
}
