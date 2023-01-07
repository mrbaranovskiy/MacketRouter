using MacketRouter.Utilities;

namespace MacketRouter.Logical;

/// <summary>
/// Represents the logical virtual element.
/// </summary>
abstract class AbstractLogicalPin : IEquatable<AbstractLogicalPin>, IEqualityComparer<AbstractLogicalPin>
{
    protected internal readonly List<ILogicalConnection> _logicalConnections = new();
    public required PinDescription Description { get; set; }
    abstract public IReadOnlyCollection<ILogicalConnection> Connections { get; }

    public bool Equals(AbstractLogicalPin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _logicalConnections.Equals(other._logicalConnections) && Description.Equals(other.Description) && Connections.Equals(other.Connections);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AbstractLogicalPin) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_logicalConnections, Description, Connections);
    }

    public bool Equals(AbstractLogicalPin? x, AbstractLogicalPin? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x._logicalConnections.Equals(y._logicalConnections) && x.Description.Equals(y.Description) && x.Connections.Equals(y.Connections);
    }

    public int GetHashCode(AbstractLogicalPin obj)
    {
        return HashCode.Combine(obj._logicalConnections, obj.Description, obj.Connections);
    }

    public virtual void ConnectTo(AbstractLogicalPin pin)
    {
        if (pin == null) throw new ArgumentNullException(nameof(pin));
        var newInConnection = new LogicalConnection(this, pin);
        _logicalConnections.Add(newInConnection);

        var newOutConnection = new LogicalConnection(pin, this);
        pin._logicalConnections.Add(newOutConnection);
    }

    public virtual void ConnectTo(params AbstractLogicalPin[] pins)
    {
        foreach (var pin in pins) ConnectTo(pin);
    }

    public virtual void DisconnectFrom(AbstractLogicalPin pin2Delete)
    {
        foreach (var logicalConnection in _logicalConnections.Where(s=>s.Reference == pin2Delete))
            logicalConnection.Reference._logicalConnections.SafeDeleteReferenced(this);

        _logicalConnections.SafeDeleteReferenced(pin2Delete);
    }

    public virtual void DisconnectFrom(params AbstractLogicalPin[] pins2Delete)
    {
        foreach (var pin in pins2Delete) 
            DisconnectFrom(pin);
    }
}