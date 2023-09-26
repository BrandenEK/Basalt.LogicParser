namespace LogicParser.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var inventory = new TestInventory();

            Assert.IsTrue(LogicParser.Evaluate(inventory, "item3"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var inventory = new TestInventory();

            Assert.IsFalse(LogicParser.Evaluate(inventory, "item1"));
        }
    }
}