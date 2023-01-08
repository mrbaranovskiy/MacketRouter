namespace MacketRouter.Logical;

internal class LogicalPin : AbstractLogicalPin
{
    public override ILogicalConnection? Connection => _logicalConnection;
}