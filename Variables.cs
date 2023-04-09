using System;

namespace LogicParser
{
    public class IntVariable : Variable
    {
        public int value;

        public IntVariable(int value)
        {
            this.value = value;
        }
    }

    public class BoolVariable : Variable
    {
        public bool value;

        public BoolVariable(bool value)
        {
            this.value = value;
        }
    }
}
