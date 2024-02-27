# Randomizer Logic Parser

Allows you to define a custom inventory that can evaluate itself using string expressions

```cs
internal class Program
{
    static void Main(string[] args)
    {
        var inventory = new TestInventory();

        string logic1 = "bool && number > 5";
        string logic2 = "(bool + number >= 1) | number >= 2";

        Console.WriteLine($"Logic 1: {inventory.Evaluate(logic1)}");
        Console.WriteLine($"Logic 2: {inventory.Evaluate(logic2)}");
        
        Console.WriteLine("Adding bool item");
        inventory.AddItem("b");
        
        Console.WriteLine($"Logic 1: {inventory.Evaluate(logic1)}");
        Console.WriteLine($"Logic 2: {inventory.Evaluate(logic2)}");
    }
}
```

```cs
public class TestInventory : InventoryData
{
    private bool boolItem = false;
    private int numberItem = 0;

    public void AddItem(string item)
    {
        switch (item)
        {
            case "b":
                boolItem = true;
                break;
            case "n":
                numberItem++;
                break;
        }
    }

    protected override object GetVariable(string variable)
    {
        return variable switch
        {
            "bool" => boolItem,
            "number" => numberItem,

            _ => throw new LogicParserException($"Unknown variable: {variable}")
        };
    }
}
```

## Supported operators
- Parenthesis: ```()``` or ```[]```
- And: ```+``` or ```&&```
- Or: ```|``` or ```||```
- Less than: ```<```
- Greater than: ```>```
- Less than or equal: ```<=```
- Greater than or equal: ```>=```
