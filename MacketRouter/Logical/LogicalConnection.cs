namespace MacketRouter.Logical;

class LogicalConnection : ILogicalConnection
{
    public LogicalConnection(AbstractLogicalPin owner, AbstractLogicalPin reference)
    {
        Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        Reference = reference ?? throw new ArgumentNullException(nameof(reference));
        
        if (Owner == Reference)
            throw new ArgumentException("Owner equals reference");
    }
    
    public AbstractLogicalPin Owner { get; }
    public AbstractLogicalPin Reference { get; }
}