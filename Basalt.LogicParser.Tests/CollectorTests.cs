using Basalt.LogicParser.Collectors;
using static Basalt.LogicParser.Tests.TestUtils;

namespace Basalt.LogicParser.Tests;

public abstract class CollectorTests
{
    protected ICollector? _collector;
    private ICollector Collector => _collector ?? throw new NullReferenceException(nameof(_collector));

    [TestMethod]
    [DataRow(new string[] { }, "fff00")]
    public void Execute(string[] input, string output)
    {
        Collector.Add("");
    }
}

[TestClass]
public class ReflectionCollectorTests : CollectorTests
{
    [TestInitialize]
    public void Setup()
    {
        _collector = new ReflectionCollector(new TestInventoryInfo());
    }
}
