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
}