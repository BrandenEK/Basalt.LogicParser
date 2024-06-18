using Basalt.LogicParser.Calculators;
using Basalt.LogicParser.Collectors;
using Basalt.LogicParser.Formatters;
using Basalt.LogicParser.Parsers;
using Basalt.LogicParser.Resolvers;
using static Basalt.LogicParser.Tests.TestUtils;

namespace Basalt.LogicParser.Tests;

public abstract class FullTests
{
    protected GameInventory? _inventory;
    private GameInventory Inventory => _inventory ?? throw new NullReferenceException(nameof(_inventory));

    [TestMethod]
    [DataRow("", OutputType.True)]
    [DataRow("+", OutputType.Error)]
    [DataRow("a", OutputType.False)]
    [DataRow("x > 0", OutputType.False)]
    [DataRow("a | x > 0", OutputType.False)]
    [DataRow("a + b", OutputType.False)]
    [DataRow("x > 0 + y > 0", OutputType.False)]
    [DataRow("(x >= 2 + a) | (y > 1 + c) + b", OutputType.False)]
    [DataRow("a && b && c || y >= 1", OutputType.False)]
    [DataRow("a + (y > 0 | b + (x >= 1 | c))", OutputType.False)]
    public void InventoryWithNothing(string expression, OutputType output)
    {
        TestExpression(expression, output);
    }

    [TestMethod]
    [DataRow("", OutputType.True)]
    [DataRow("+", OutputType.Error)]
    [DataRow("a", OutputType.True)]
    [DataRow("x > 0", OutputType.False)]
    [DataRow("a | x > 0", OutputType.True)]
    [DataRow("a + b", OutputType.False)]
    [DataRow("x > 0 + y > 0", OutputType.False)]
    [DataRow("(x >= 2 + a) | (y > 1 + c) + b", OutputType.False)]
    [DataRow("a && b && c || y >= 1", OutputType.False)]
    [DataRow("a + (y > 0 | b + (x >= 1 | c))", OutputType.False)]
    public void InventoryWithA(string expression, OutputType output)
    {
        Inventory.Add("a");

        TestExpression(expression, output);
    }

    [TestMethod]
    [DataRow("", OutputType.True)]
    [DataRow("+", OutputType.Error)]
    [DataRow("a", OutputType.False)]
    [DataRow("x > 0", OutputType.True)]
    [DataRow("a | x > 0", OutputType.True)]
    [DataRow("a + b", OutputType.False)]
    [DataRow("x > 0 + y > 0", OutputType.False)]
    [DataRow("(x >= 2 + a) | (y > 1 + c) + b", OutputType.False)]
    [DataRow("a && b && c || y >= 1", OutputType.False)]
    [DataRow("a + (y > 0 | b + (x >= 1 | c))", OutputType.False)]
    public void InventoryWithX(string expression, OutputType output)
    {
        Inventory.Add("x");

        TestExpression(expression, output);
    }

    [TestMethod]
    [DataRow("", OutputType.True)]
    [DataRow("+", OutputType.Error)]
    [DataRow("a", OutputType.True)]
    [DataRow("x > 0", OutputType.True)]
    [DataRow("a | x > 0", OutputType.True)]
    [DataRow("a + b", OutputType.True)]
    [DataRow("x > 0 + y > 0", OutputType.True)]
    [DataRow("(x >= 2 + a) | (y > 1 + c) + b", OutputType.True)]
    [DataRow("a && b && c || y >= 1", OutputType.True)]
    [DataRow("a + (y > 0 | b + (x >= 1 | c))", OutputType.True)]
    public void InventoryWithEverything(string expression, OutputType output)
    {
        Inventory.Add("a");
        Inventory.Add("b");
        Inventory.Add("c");
        Inventory.Add("x");
        Inventory.Add("x");
        Inventory.Add("y");
        Inventory.Add("y");

        TestExpression(expression, output);
    }

    private void TestExpression(string expression, OutputType output)
    {
        switch (output)
        {
            case OutputType.True:
                Assert.IsTrue(Inventory.Evaluate(expression));
                break;
            case OutputType.False:
                Assert.IsFalse(Inventory.Evaluate(expression));
                break;
            case OutputType.Error:
                Assert.ThrowsException<LogicParserException>(() => Inventory.Evaluate(expression));
                break;
        }
    }
}

[TestClass]
public class LegacyInventoryTests : FullTests
{
    [TestInitialize]
    public void Setup()
    {
        TestInventoryInfo info = new();

        _inventory = new GameInventory(
            new PostfixCalculator(),
            new LegacyCollector(info),
            new ParenthesisPaddingFormatter(),
            new PostfixParser(),
            new LegacyResolver(info));
    }
}

[TestClass]
public class NewInventoryTests : FullTests
{
    [TestInitialize]
    public void Setup()
    {
        TestInventoryInfo info = new();

        _inventory = new GameInventory(
            new PostfixCalculator(),
            new ReflectionCollector(info),
            new ParenthesisPaddingFormatter(),
            new PostfixParser(),
            new ReflectionResolver(info));
    }
}
