using System;

namespace LogicParser
{
    internal abstract class Token
    {

    }

    internal abstract class Variable : Token
    {

    }

    internal abstract class Operator : Token
    {
        public abstract string Name { get; }
        public abstract byte Order { get; }

        public abstract bool Evaluate(Variable left, Variable right);
    }
}
