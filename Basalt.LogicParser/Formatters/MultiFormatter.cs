using System.Collections.Generic;

namespace Basalt.LogicParser.Formatters;

/// <summary>
/// Performs multiple format operations one after another
/// </summary>
public class MultiFormatter(IEnumerable<IFormatter> formatters) : IFormatter
{
    private readonly IEnumerable<IFormatter> _formatters = formatters;

    /// <inheritdoc/>
    public string Format(string expression)
    {
        foreach (var formatter in _formatters)
            expression = formatter.Format(expression);
        return expression;
    }
}
