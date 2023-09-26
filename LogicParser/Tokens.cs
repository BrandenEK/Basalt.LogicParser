
namespace LogicParser
{
    internal abstract class Token { }

    internal abstract class Variable : Token { }

    internal abstract class Operator : Token
    {
        public readonly string name; // Maybe not necessary
        public readonly byte order;

        public Operator(string name, byte order)
        {
            this.name = name;
            this.order = order;
        }

        public abstract bool Evaluate(Variable left, Variable right);
    }
}
