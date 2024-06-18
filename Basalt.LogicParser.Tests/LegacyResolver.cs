using Basalt.LogicParser.Models;
using Basalt.LogicParser.Resolvers;

namespace Basalt.LogicParser.Tests;

internal class LegacyResolver(TestInventoryInfo info) : IResolver
{
    private readonly TestInventoryInfo _info = info;

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
