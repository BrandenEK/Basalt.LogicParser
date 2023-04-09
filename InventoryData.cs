using System;

namespace LogicParser
{
    public abstract class InventoryData
    {
        protected internal abstract Variable GetVariable(string variable);
    }
}
