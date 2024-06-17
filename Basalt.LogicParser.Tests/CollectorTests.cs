using Basalt.LogicParser.Collectors;
using static Basalt.LogicParser.Tests.TestUtils;

namespace Basalt.LogicParser.Tests;

public abstract class CollectorTests
{
    protected ICollector? _collector;
    private ICollector Collector => _collector ?? throw new NullReferenceException(nameof(_collector));

    [TestMethod]
    public void Execute()
    {
        Collector.Add("");
    }
}

//public abstract class CalculatorTests
//{

//    [TestMethod]
//    [DataRow("", OutputType.Error)]
//    [DataRow("t", OutputType.True)]
//    [DataRow("f", OutputType.False)]
//    [DataRow("1", OutputType.Error)]
//    [DataRow("(", OutputType.Error)]
//    [DataRow("+", OutputType.Error)]
//    [DataRow(">", OutputType.Error)]
//    [DataRow("tt", OutputType.Error)]
//    [DataRow("()", OutputType.Error)]
//    [DataRow("f+", OutputType.Error)]
//    [DataRow(")|", OutputType.Error)]
//    [DataRow("tt+", OutputType.True)]
//    [DataRow("tt|", OutputType.True)]
//    [DataRow("tf+", OutputType.False)]
//    [DataRow("ft|", OutputType.True)]
//    [DataRow("10>", OutputType.True)]
//    [DataRow("tt+f|", OutputType.True)]
//    public void Calculate_Postfix(string input, OutputType output)
//    {
//        var tokens = ParseString(input);
//        switch (output)
//        {
//            case OutputType.True:
//                Assert.IsTrue(Calculator.Calculate(tokens));
//                break;
//            case OutputType.False:
//                Assert.IsFalse(Calculator.Calculate(tokens));
//                break;
//            case OutputType.Error:
//                Assert.ThrowsException<LogicParserException>(() => Calculator.Calculate(tokens));
//                break;
//        }
//    }
//}

[TestClass]
public class ReflectionCollectorTests : CollectorTests
{
    [TestInitialize]
    public void Setup()
    {
        _collector = new ReflectionCollector(new object());
    }
}
