
namespace Basalt.LogicParser.Collectors;

/// <summary>
/// Collects an item and either adds it to or removes it from the inventory
/// </summary>
public interface ICollector
{
    /// <summary>
    /// Adds the item to the inventory
    /// </summary>
    public void Add(string item);

    /// <summary>
    /// Removes the item from the inventory
    /// </summary>
    public void Remove(string item);
}
