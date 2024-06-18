using Basalt.LogicParser.Models;

namespace Basalt.LogicParser.Tests;

public class TestUtils
{
    public static IEnumerable<Token> ParseString(string str)
    {
        return str.Select(ParseChar);
    }

    public static Token ParseChar(char c)
    {
        return c switch
        {
            't' => new BoolVariable(true),
            'f' => new BoolVariable(false),
            '0' => new IntVariable(0),
            '1' => new IntVariable(1),
            '(' => new LeftParenthesisOperator(),
            ')' => new RightParenthesisOperator(),
            '+' => new AndOperator(),
            '|' => new OrOperator(),
            '<' => new LessOperator(),
            '>' => new GreaterOperator(),
            _ => throw new NotImplementedException()
        };
    }

    public enum OutputType
    {
        True,
        False,
        Error
    }
}
