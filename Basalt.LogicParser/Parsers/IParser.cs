
using System.Collections.Generic;

namespace Basalt.LogicParser.Parsers;

public interface IParser
{
    public IEnumerable<Token> Parse(string expression);
}
