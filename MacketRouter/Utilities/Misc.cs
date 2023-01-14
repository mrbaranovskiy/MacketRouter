using MacketRouter.DataStructures;
using MacketRouter.Logical.LogicalElements;

namespace MacketRouter.Utilities;

internal static class Misc
{
    public static ElementRect ElementRect(this ILogicalElement element) => element switch
    {
        LogicalResistor resistor and {Name: "lala"} => new ElementRect()
    };
}