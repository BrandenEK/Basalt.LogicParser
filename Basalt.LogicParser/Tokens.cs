
namespace Basalt.LogicParser;

public abstract class Token { }


internal abstract class Operator : Token
{
    public readonly byte order;

    public Operator(byte order)
    {
        this.order = order;
    }

    public abstract bool Evaluate(Variable left, Variable right);
}
