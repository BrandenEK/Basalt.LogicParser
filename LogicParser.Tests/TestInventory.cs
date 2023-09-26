
namespace LogicParser.Tests
{
    internal class TestInventory : InventoryData
    {
        private bool item1 = false;
        private bool item2 = true;
        private bool item3 = true;

        private int numbers1 = 0;
        private int numbers2 = 5;

        protected override object GetVariable(string variable)
        {
            return variable switch
            {
                "item1" => item1,
                "item2" => item2,
                "item3" => item3,

                "numbers1" => numbers1,
                "numbers2" => numbers2,

                _ => throw new LogicParserException($"Unknown variable: {variable}")
            };
        }
    }
}
