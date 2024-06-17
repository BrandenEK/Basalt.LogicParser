using Basalt.LogicParser.Calculators;
using Basalt.LogicParser.Formatters;
using Basalt.LogicParser.Parsers;

namespace Basalt.LogicParser.Tests;

[TestClass]
public class ExpressionTests
{
    private static GameInventory CreateNewInventory()
    {
        TestInventoryInfo info = new();

        return new GameInventory(
            new PostfixCalculator(),
            new ParenthesisPaddingFormatter(),
            new PostfixParser(),
            new TestResolver(info));
    }

    [TestMethod]
    public void A_BeforeItem()
    {
        var inventory = CreateNewInventory();

        Assert.IsFalse(inventory.Evaluate("a"));
    }

    [TestMethod]
    public void A_AfterItem()
    {
        var inventory = CreateNewInventory();
        inventory.Add("a");

        Assert.IsTrue(inventory.Evaluate("a"));
    }

    [TestMethod]
    public void X_BeforeItem()
    {
        var inventory = CreateNewInventory();

        Assert.IsFalse(inventory.Evaluate("x > 0"));
    }

    [TestMethod]
    public void X_AfterItem()
    {
        var inventory = CreateNewInventory();
        inventory.Add("x");

        Assert.IsTrue(inventory.Evaluate("x > 0"));
    }

    [TestMethod]
    public void ComplexLogic()
    {
        var inventory = CreateNewInventory();
        inventory.Add("a");
        inventory.Add("b");
        inventory.Add("x");
        inventory.Add("x");

        bool expr1 = inventory.Evaluate("(x >= 2 + a) | (y > 1 + c) + b");
        bool expr2 = inventory.Evaluate("a && b && c || y >= 1");
        bool expr3 = inventory.Evaluate("a + (y > 0 | b + (x >= 1 | c))");

        Assert.IsTrue(expr1 && !expr2 && expr3);
    }
}