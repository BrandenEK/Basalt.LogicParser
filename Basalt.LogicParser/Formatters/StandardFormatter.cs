using System;

namespace Basalt.LogicParser.Formatters;

/// <inheritdoc/>
public class StandardFormatter : IFormatter
{
    /// <inheritdoc/>
    public string Format(string expression)
    {
        return AddPaddingAroundTokens(expression);
    }

    /// <summary>
    /// Adds padding around parentheses, if not already present
    /// </summary>
    private string AddPaddingAroundTokens(string expression)
    {
        for (int i = 0; i < expression.Length; i++)
        {
            if (expression[i] == '(' || expression[i] == ')')
            {
                if (i > 0 && expression[i - 1] != ' ')
                {
#if NET35
                    expression = expression.Substring(0, i) + ' ' + expression.Substring(i);
#elif NET6_0_OR_GREATER
                    expression = string.Concat(expression.AsSpan(0, i), " ", expression.AsSpan(i));
#endif
                    i++;
                }
                if (i < expression.Length - 1 && expression[i + 1] != ' ')
                {
#if NET35
                    expression = expression.Substring(0, i + 1) + ' ' + expression.Substring(i + 1);
#elif NET6_0_OR_GREATER
                    expression = string.Concat(expression.AsSpan(0, i + 1), " ", expression.AsSpan(i + 1));
#endif
                    i++;
                }
            }
        }

        return expression;
    }
}
