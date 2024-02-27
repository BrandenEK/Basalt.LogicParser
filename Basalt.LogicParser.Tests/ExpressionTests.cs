
namespace Basalt.LogicParser.Tests
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void Item1_BeforeItem()
        {
            var inventory = new TestInventory();

            Assert.IsFalse(inventory.Evaluate("item1"));
        }

        [TestMethod]
        public void Item1_AfterItem()
        {
            var inventory = new TestInventory();
            inventory.AddItem("item1");

            Assert.IsTrue(inventory.Evaluate("item1"));
        }

        [TestMethod]
        public void Numbers1_BeforeItem()
        {
            var inventory = new TestInventory();

            Assert.IsFalse(inventory.Evaluate("numbers1 > 0"));
        }

        [TestMethod]
        public void Numbers1_AfterItem()
        {
            var inventory = new TestInventory();
            inventory.AddItem("numbers1");

            Assert.IsTrue(inventory.Evaluate("numbers1 > 0"));
        }

        [TestMethod]
        public void ComplexLogic()
        {
            var inventory = new TestInventory();
            inventory.AddItem("item1");
            inventory.AddItem("item2");
            inventory.AddItem("numbers1");
            inventory.AddItem("numbers1");

            bool expr1 = inventory.Evaluate("(numbers1 >= 2 + item1) | (numbers2 > 1 + item3) + item2");
            bool expr2 = inventory.Evaluate("item1 && item2 && item3 || numbers2 >= 1");
            bool expr3 = inventory.Evaluate("item1 + (numbers2 > 0 | item2 + (numbers1 >= 1 | item3))");

            Assert.IsTrue(expr1 && !expr2 && expr3);
        }
    }
}