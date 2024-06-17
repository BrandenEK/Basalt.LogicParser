using Basalt.LogicParser.Collectors;

namespace Basalt.LogicParser.Tests;

public class TestCollector(TestInventoryInfo info) : ICollector
{
    private readonly TestInventoryInfo _info = info;

    public void Add(string item)
    {
        switch (item)
        {
            case "a": _info.A = true; break;
            case "b": _info.B = true; break;
            case "c": _info.C = true; break;

            case "x": _info.X++; break;
            case "y": _info.Y++; break;
        }
    }

    public void Remove(string item)
    {
        switch (item)
        {
            case "a": _info.A = false; break;
            case "b": _info.B = false; break;
            case "c": _info.C = false; break;

            case "x": _info.X--; break;
            case "y": _info.Y--; break;
        }
    }
}
