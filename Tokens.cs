using System;

namespace LogicParser
{
    public abstract class Token
    {

    }

    public abstract class Variable : Token
    {

    }

    public abstract class Operator : Token
    {
        public abstract string Name { get; }
        public abstract byte Order { get; }

        public abstract bool Evaluate(Variable left, Variable right);
    }
}
