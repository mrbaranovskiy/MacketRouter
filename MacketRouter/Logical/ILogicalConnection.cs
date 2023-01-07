namespace MacketRouter.Logical;

/// <summary>
/// Represents the virtual connection
/// </summary>
interface ILogicalConnection
{
    AbstractLogicalPin Owner { get; }
    AbstractLogicalPin Reference { get; }
}