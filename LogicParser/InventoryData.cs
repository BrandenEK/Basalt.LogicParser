
namespace LogicParser
{
    public abstract class InventoryData
    {
        internal Variable GetVariableValue(string variable)
        {
            object value = GetVariable(variable);
            switch (value)
            {
                case bool b:
                    return new BoolVariable(b);
                case int i:
                    return new IntVariable(i);
                default:
                    throw new LogicParserException($"Unsupported variable type: {value.GetType().Name}");
            }
        }

        protected abstract object GetVariable(string variable);
    }
}
