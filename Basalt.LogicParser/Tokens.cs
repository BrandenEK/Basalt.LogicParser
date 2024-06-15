
namespace Basalt.LogicParser;

internal abstract class Token { }

internal abstract class Variable : Token { }

internal abstract class Operator : Token
{
    public readonly byte order;

    public Operator(byte order)
    {
        this.order = order;
    }

    public abstract bool Evaluate(Variable left, Variable right);
}
