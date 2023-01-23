
using MacketRouter.Logical;
using MacketRouter.Logical.LogicalElements;

namespace MacketRouter;

class TopologyTraverse
{
    public static ILogicalElement? FindRelative(ILogicalElement from, string toFind)
    {
        var visited = new HashSet<ILogicalElement>();
        var queue = new Queue<ILogicalElement>();
        visited.Add(from);
        queue.Enqueue(from);

        if (from.Name == toFind) return from;

        while (queue.Count > 0)
        {
            var element = queue.Dequeue();

            foreach (var thisElPin in element.Pins)
            {
                if(thisElPin.Connection is null) continue;
                
                foreach (var otherPin in thisElPin.Connection.ConnectedPins.Where(p=> p != thisElPin))
                {
                    var owner = PinOwner(otherPin);
                    
                    if(visited.Contains(owner)) continue;
                    
                    if (owner.Name == toFind)
                        return owner;

                    visited.Add(owner);
                    queue.Enqueue(owner);
                }
            }
        }

        return null;

        ILogicalElement PinOwner(AbstractLogicalPin pin)
        {
            return pin.Owner;
        }
    }
}