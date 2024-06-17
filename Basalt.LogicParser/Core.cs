using Basalt.LogicParser.Attributes;
using Basalt.LogicParser.Collectors;
using System;

namespace Basalt.LogicParser;

public static class Core
{
    public static void Main(string[] args)
    {
        InventoryInfo info = new();

        ICollector collector = new ReflectionCollector(info);

        Console.WriteLine("Before:");
        info.Print();

        Console.WriteLine("Add A:");
        collector.Add("a");
        info.Print();

        Console.WriteLine("Add X:");
        collector.Add("x");
        info.Print();

        Console.WriteLine("Add X:");
        collector.Add("x");
        info.Print();

        Console.WriteLine("Remove A:");
        collector.Remove("a");
        info.Print();

        Console.ReadKey();
    }
}

public class InventoryInfo
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

    public void Print()
    {
        Console.WriteLine($"A: {A}");
        Console.WriteLine($"B: {B}");
        Console.WriteLine($"C: {C}");
        Console.WriteLine($"X: {X}");
        Console.WriteLine($"Y: {Y}");
    }
}
