namespace MacketRouter.Logical;

class LogicalConnection : ILogicalConnection
{
    private readonly List<AbstractLogicalPin> _connections;
    public LogicalConnection() => _connections = new List<AbstractLogicalPin>();

    public bool IsEmpty => !ConnectedPins.Any();

    public IReadOnlyList<AbstractLogicalPin> ConnectedPins => _connections;
    
    //todo: need to replace List with some set and remove the duplicates of the same connection.
    public void AddConnection(AbstractLogicalPin pin)
    {
        _connections.Add(pin);
    }

    public void AddConnection(params AbstractLogicalPin[] pins)
    {
        foreach (var p in pins) AddConnection(p);
    }

    public void RemoveConnection(AbstractLogicalPin pin)
    {
        _connections.Remove(pin);
    }

    public void RemoveConnection(params AbstractLogicalPin[] pins)
    {
        foreach (var pin in pins) RemoveConnection(pin);
    }
}