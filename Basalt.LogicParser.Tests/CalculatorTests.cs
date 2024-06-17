using Basalt.LogicParser.Calculators;
using static Basalt.LogicParser.Tests.TestUtils;

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
}

[TestClass]
public class PostfixCalculatorTests : CalculatorTests
{
    protected override ICalculator Calculator { get; } = new PostfixCalculator();
}
