using MacketRouter.Logical.LogicalElements;

namespace MacketRouter.Logical;

/// <summary>
/// Represents the logical virtual element.
/// </summary>
abstract class AbstractLogicalPin  : IEquatable<AbstractLogicalPin>, IEqualityComparer<AbstractLogicalPin>
{
    protected internal ILogicalConnection? _logicalConnection;
    public required PinDescription Description { get; set; }
    abstract public ILogicalConnection? Connection { get; }

    public ILogicalElement Owner { get; private set; }

    /// <summary>
    /// Can be excecuted once
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void SetOwner(ILogicalElement owner)
    {
        if (Owner is { })
            throw new InvalidOperationException("Owner is already set");
        if (owner is null)
            throw new ArgumentNullException(nameof(owner));

        Owner = owner;
    }

    public virtual void ConnectTo(AbstractLogicalPin pin)
    {
        if (pin == null) throw new ArgumentNullException(nameof(pin));

        if (pin.Connection is { } conn)
        {
            _logicalConnection = conn;
            _logicalConnection.AddConnection(this);
        }
        else
        {
            if (_logicalConnection is not { })
                _logicalConnection = new LogicalConnection();

            _logicalConnection!.AddConnection(this, pin);
            pin.SetConnection(_logicalConnection);
        }
    }

    public virtual void ConnectTo(params AbstractLogicalPin[] pins)
    {
        foreach (var pin in pins) ConnectTo(pin);
    }

    public virtual void DisconnectFrom(AbstractLogicalPin pin2Delete)
    {
        if (pin2Delete == null) throw new ArgumentNullException(nameof(pin2Delete));
        
        _logicalConnection?.RemoveConnection(pin2Delete);
    }

    public virtual void DisconnectFrom(params AbstractLogicalPin[] pins2Delete)
    {
        foreach (var pin in pins2Delete) 
            DisconnectFrom(pin);
    }

    public override string ToString()
    {
        return $"{Description.Description} ->" + 
            Connection?.ConnectedPins.Select(s => s.Description.Description)
            .Aggregate((m,n) => m + "," + n) ?? nameof(AbstractLogicalPin);
    }

    private void SetConnection(ILogicalConnection parent)
    {
        if (parent is { } && _logicalConnection is { } && !parent.Equals(_logicalConnection))
        {
            MergeConnections(parent, _logicalConnection);
            _logicalConnection = parent;
        }
        else
        {
            _logicalConnection ??= parent ?? throw new ArgumentNullException(nameof(parent));
        }
    }

    private void MergeConnections(ILogicalConnection parent, ILogicalConnection child)
    {
        foreach (var conn in child.ConnectedPins) 
            parent.AddConnection(conn);
    }

    public bool Equals(AbstractLogicalPin? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(_logicalConnection, other._logicalConnection) && Description.Equals(other.Description) && Equals(Connection, other.Connection);
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
        return HashCode.Combine(_logicalConnection, Description, Connection);
    }

    public bool Equals(AbstractLogicalPin? x, AbstractLogicalPin? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return Equals(x._logicalConnection, y._logicalConnection) && x.Description.Equals(y.Description) && Equals(x.Connection, y.Connection);
    }

    public int GetHashCode(AbstractLogicalPin obj)
    {
        return HashCode.Combine(obj._logicalConnection, obj.Description, obj.Connection);
    }
}