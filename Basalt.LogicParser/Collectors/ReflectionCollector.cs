
namespace Basalt.LogicParser.Collectors;

/// <summary>
/// Adds or removes items based on collectable attributes on the info object
/// </summary>
public class ReflectionCollector(object info) : ICollector
{
    private readonly object _info = info;

    /// <inheritdoc/>
    public void Add(string item)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public void Remove(string item)
    {
        throw new System.NotImplementedException();
    }
}
