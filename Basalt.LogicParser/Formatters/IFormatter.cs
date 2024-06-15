
namespace Basalt.LogicParser.Formatters;

/// <summary>
/// Formats the logic expression before it is parsed
/// </summary>
public interface IFormatter
{
    /// <summary>
    /// Formats the logic expression before it is parsed
    /// </summary>
    public string Format(string expression);
}
