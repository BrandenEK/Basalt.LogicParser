
using Basalt.LogicParser.Attributes;

namespace Basalt.LogicParser.Tests;

public class TestInventoryInfo
{
    [CollectableAs("a")]
    public bool A { get; set; }

    [CollectableAs("b")]
    public bool B { get; set; }

    [CollectableAs("c")]
    public bool C { get; set; }

    [CollectableAs("x")]
    public int X { get; set; }

    [CollectableAs("y")]
    public int Y { get; set; }
}
