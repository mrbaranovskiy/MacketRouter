#region Information

// File IHub.cs has been created by: Dmytro Baranovskyi at: 2023 01 23

// Description: 
#endregion

using System.Collections.Immutable;

namespace MacketRouter.Physical;

interface IHub
{
    public static int MaxBlockHeight { get; }
    public static int MaxBlockWidth { get; }

    ImmutableHashSet<PinPoint> FreePins { get; }
    ImmutableDictionary<ElementPin, PinPoint> Connections { get; }

    void AddConnection(string elementName, PinPoint pinPoint);
    void RemoveConnection(PinPoint pinPoint);
    void UpdateConnection(string name, PinPoint newPinPoint);

    void ExtendWithPins(IEnumerable<PinPoint> newPins);
}