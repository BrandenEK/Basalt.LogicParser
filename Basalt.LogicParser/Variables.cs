
namespace Basalt.LogicParser;

internal class IntVariable : Variable
{
    public readonly int value;

    public IntVariable(int value) => this.value = value;
}

internal class BoolVariable : Variable
{
    public readonly bool value;

    public BoolVariable(bool value) => this.value = value;
}
