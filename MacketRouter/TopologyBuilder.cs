using MacketRouter.Logical.LogicalElements;

namespace MacketRouter;

internal enum LogicalElementType
{
    Resistor, Capasitor, Inductor, Diod, Groud
}

internal sealed class TopologyBuilder
{
    private HashSet<ILogicalElement> _registry = new HashSet<ILogicalElement>();

    private ILogicalElement CreateElementInternal(LogicalElementType type, string name)
    {
        switch (type)
        {
            case LogicalElementType.Resistor:
                return new LogicalResistor {Name = name};
            case LogicalElementType.Capasitor:
                return new LogicalCapasitor {Name = name};
            case LogicalElementType.Inductor:
                return new LogicalInductor {Name = name};
            case LogicalElementType.Diod:
                return new LogicalDiod {Name = name};
            case LogicalElementType.Groud:
                return new LogicalGround {Name = name};
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public ILogicalElement CreateElement(LogicalElementType type, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        
        var newItem = CreateElementInternal(type, name);
        _registry.Add(newItem);

        return newItem;
    }

    public bool Contains(ILogicalElement element)
    {
        return _registry.Contains(element);
    }
}