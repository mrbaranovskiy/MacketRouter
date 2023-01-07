namespace MacketRouter.Logical;

internal class LogicalPin : AbstractLogicalPin
{
    public override IReadOnlyCollection<ILogicalConnection> Connections => _logicalConnections;
}