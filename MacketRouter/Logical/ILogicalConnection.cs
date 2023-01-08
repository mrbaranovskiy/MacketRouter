namespace MacketRouter.Logical;

/// <summary>
/// Represents the virtual connection
/// </summary>
interface ILogicalConnection
{
    bool IsEmpty {get;}
    IReadOnlyList<AbstractLogicalPin> ConnectedPins { get; }
    void AddConnection(AbstractLogicalPin pin);
    void AddConnection(params AbstractLogicalPin[] pins);
    void RemoveConnection(AbstractLogicalPin pin);
    void RemoveConnection(params AbstractLogicalPin[] pins );
}