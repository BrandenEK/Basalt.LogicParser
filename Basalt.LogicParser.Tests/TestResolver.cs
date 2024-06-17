using Basalt.LogicParser.Models;
using Basalt.LogicParser.Resolvers;

namespace Basalt.LogicParser.Tests;

internal class TestResolver(TestInventoryInfo info) : IResolver
{
    private readonly TestInventoryInfo _info = info;

    public void AddItem(string item)
    {
        switch (item)
        {
            case "item1":
                item1 = true;
                break;
            case "item2":
                item2 = true;
                break;
            case "item3":
                item3 = true;
                break;
            case "numbers1":
                numbers1++;
                break;
            case "numbers2":
                numbers2++;
                break;
        }
    }

    public Variable Resolve(string variable)
    {
        return variable switch
        {
            "a" => new BoolVariable(_info.A),
            "b" => new BoolVariable(_info.B),
            "c" => new BoolVariable(_info.C),

            "x" => new IntVariable(_info.X),
            "y" => new IntVariable(_info.Y),

            _ => throw new LogicParserException($"Unknown variable: {variable}")
        };
    }
}
