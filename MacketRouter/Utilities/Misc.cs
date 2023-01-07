using MacketRouter.Logical;

namespace MacketRouter.Utilities;

internal static class Misc
{
    internal static void SafeDeleteReferenced(this IList<ILogicalConnection> connections,
        AbstractLogicalPin pin)
    {
        foreach (var item in connections.Where(s => s.Reference == pin).ToArray())
        {
            connections.Remove(item);
        }
    }
}