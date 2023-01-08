using System.ComponentModel;
using System.Runtime.CompilerServices;
using MacketRouter.Utilities;

namespace MacketRouter.Logical;

/// <summary>
/// Represents the logical virtual element.
/// </summary>
abstract class AbstractLogicalPin // : IEquatable<AbstractLogicalPin>, IEqualityComparer<AbstractLogicalPin>
{
    protected internal ILogicalConnection? _logicalConnection;
    public required PinDescription Description { get; set; }
    abstract public ILogicalConnection? Connections { get; }

    public virtual void ConnectTo(AbstractLogicalPin pin)
    {
        if (pin == null) throw new ArgumentNullException(nameof(pin));
        
        if (_logicalConnection is not { })
            _logicalConnection = new LogicalConnection();

        _logicalConnection!.AddConnection(this, pin);

        pin.SetConnection(_logicalConnection);
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
        return Description.Description + "::" + Connections?.ConnectedPins.Select(s => s.Description.Description).Aggregate((m,n) => m + "||" +n) ?? nameof(AbstractLogicalPin);
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
}