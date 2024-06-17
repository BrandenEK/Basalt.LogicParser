using Basalt.LogicParser.Resolvers;

namespace Basalt.LogicParser.Tests;

internal class TestResolver : LegacyResolver
{
    private bool item1 = false;
    private bool item2 = false;
    private bool item3 = false;

    private int numbers1 = 0;
    private int numbers2 = 0;

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
