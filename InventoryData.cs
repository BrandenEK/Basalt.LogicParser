using System.Collections.Generic;

namespace LogicParser
{
    public class InventoryData
    {
        internal Dictionary<string, bool> Booleans { get; private set; }
        internal Dictionary<string, int> Integers { get; private set; }

        public InventoryData(Dictionary<string, bool> booleans, Dictionary<string, int> integers)
        {
            Booleans = booleans;
            Integers = integers;
        }

        internal bool IsVariable(string name, out Variable var)
        {
            if (Booleans.ContainsKey(name))
            {
                var = new BoolVariable(Booleans[name]);
                return true;
            }
            if (Integers.ContainsKey(name))
            {
                var = new IntVariable(Integers[name]);
                return true;
            }
            var = null;
            return false;
        }

        public void ActivateBoolean(string name)
        {
            if (Booleans.ContainsKey(name))
                Booleans[name] = true;
        }
        public void DeactivateBoolean(string name)
        {
            if (Booleans.ContainsKey(name))
                Booleans[name] = false;
        }

        public void IncreaseInteger(string name)
        {
            if (Integers.ContainsKey(name))
                Integers[name]++;
        }
        public void DecreaseInteger(string name)
        {
            if (Integers.ContainsKey(name))
                Integers[name]--;
        }
    }
}
