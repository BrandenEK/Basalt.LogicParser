using Basalt.LogicParser.Attributes;

namespace Basalt.LogicParser.Tests;

public class TestInventoryInfo
{
    [CollectableAs("a")] [ResolvableAs("a")]
    public bool A { get; set; }

    [CollectableAs("b")] [ResolvableAs("b")]
    public bool B { get; set; }

    [CollectableAs("c")] [ResolvableAs("c")]
    public bool C { get; set; }

    [CollectableAs("x")] [ResolvableAs("x")]
    public int X { get; set; }

    [CollectableAs("y")] [ResolvableAs("y")]
    public int Y { get; set; }
}
