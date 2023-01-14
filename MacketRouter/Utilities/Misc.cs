using MacketRouter.DataStructures;
using MacketRouter.Logical.LogicalElements;

namespace MacketRouter.Utilities;

internal static class Misc
{
    public static ElementRect ElementRect(this ILogicalElement element, ElementPosition pos)
    {
        var mappedSize = ComponentsLibrary.MapSize(element.FrameType);   
        return new ElementRect(pos.X, pos.Y, mappedSize.Width, mappedSize.Height);
    }

    public static ElementRect Test(this ILogicalElement element, Orientation orientation)
    {
        switch (element)
        {
            case LogicalResistor {FrameType: ComponentsLibrary.FrameType.ResistorAXIAL03} resistor when
                orientation == Orientation.Horizontal && resistor.Pins.Count == 4:
                break;
        }

        return new ElementRect();
    }
}