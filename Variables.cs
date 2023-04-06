using System;

namespace LogicParser
{
    internal class IntVariable : Variable
    {
        public int value;

        public IntVariable(int value)
        {
            this.value = value;
        }
    }

    internal class BoolVariable : Variable
    {
        public bool value;

        public BoolVariable(bool value)
        {
            this.value = value;
        }
    }
}
