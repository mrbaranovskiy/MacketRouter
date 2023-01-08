namespace MacketRouter.Logical;

internal class LogicalPin : AbstractLogicalPin
{
    public override ILogicalConnection? Connections => _logicalConnection;
}