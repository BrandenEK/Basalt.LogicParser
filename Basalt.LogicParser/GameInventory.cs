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
    /// Used to calculate a boolean result from a collection of tokens
    /// </summary>
    public ICalculator Calculator { get; set; } = new StandardCalculator();

    /// <summary>
    /// Used to format the string expression before going into the parser
    /// </summary>
    public IFormatter Formatter { get; set; } = new ParenthesisPaddingFormatter();

    /// <summary>
    /// Used to parse the string expression into a collection of tokens
    /// </summary>
    public IParser Parser { get; set; } = new StandardParser();

    /// <summary>
    /// Used to resolve a string variable into a token
    /// </summary>
    public IResolver Resolver { get; set; } = new EmptyResolver();

    /// <summary>
    /// Converts the string expression to a logical statement and evaluates it
    /// </summary>
    public bool Evaluate(string expression)
    {
        LogicParserException.CurrentExpression = expression;

        return string.IsNullOrEmpty(expression) || Calculator.Calculate(Parser.Parse(Formatter.Format(expression), Resolver));
    }
}
