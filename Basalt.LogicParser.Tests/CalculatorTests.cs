using Basalt.LogicParser.Calculators;
using Basalt.LogicParser.Models;

namespace Basalt.LogicParser.Tests;

public abstract class CalculatorTests
{
    protected abstract ICalculator Calculator { get; }

    [TestMethod]
    [DataRow("", OutputType.Error)]
    [DataRow("t", OutputType.True)]
    [DataRow("f", OutputType.False)]
    [DataRow("1", OutputType.Error)]
    [DataRow("(", OutputType.Error)]
    [DataRow("+", OutputType.Error)]
    [DataRow(">", OutputType.Error)]
    [DataRow("tt", OutputType.Error)]
    [DataRow("()", OutputType.Error)]
    [DataRow("f+", OutputType.Error)]
    [DataRow(")|", OutputType.Error)]
    [DataRow("tt+", OutputType.True)]
    [DataRow("tt|", OutputType.True)]
    [DataRow("tf+", OutputType.False)]
    [DataRow("ft|", OutputType.True)]
    [DataRow("10>", OutputType.True)]
    [DataRow("tt+f|", OutputType.True)]
    public void Calculate_Postfix(string input, OutputType output)
    {
        var tokens = ParseString(input);
        switch (output)
        {
            case OutputType.True:
                Assert.IsTrue(Calculator.Calculate(tokens));
                break;
            case OutputType.False:
                Assert.IsFalse(Calculator.Calculate(tokens));
                break;
            case OutputType.Error:
                Assert.ThrowsException<LogicParserException>(() => Calculator.Calculate(tokens));
                break;
        }
    }

    private static IEnumerable<Token> ParseString(string str)
    {
        return str.Select(ParseChar);
    }

    private static Token ParseChar(char c)
    {
        return c switch
        {
            't' => new BoolVariable(true),
            'f' => new BoolVariable(false),
            '0' => new IntVariable(0),
            '1' => new IntVariable(1),
            '(' => new LeftParenthesisOperator(),
            ')' => new RightParenthesisOperator(),
            '+' => new AndOperator(),
            '|' => new OrOperator(),
            '<' => new LessOperator(),
            '>' => new GreaterOperator(),
            _ => throw new NotImplementedException()
        };
    }

    public enum OutputType
    {
        True,
        False,
        Error
    }
}

[TestClass]
public class PostfixCalculatorTests : CalculatorTests
{
    protected override ICalculator Calculator { get; } = new PostfixCalculator();
}
