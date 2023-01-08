using MacketRouter.Logical.LogicalElements;
using MacketRouter.Utilities;

namespace MacketRouter;

internal enum LogicalElementType
{
    Resistor, Capasitor, Inductor, Diod, Groud, VCC,
    Wire
}

internal sealed class TopologyBuilder
{
    private readonly HashSet<ILogicalElement> _registry;

    public TopologyBuilder()
    {
        _registry = new HashSet<ILogicalElement>();
    }

    private ILogicalElement CreateElementInternal(LogicalElementType type, string name)
    {
        return type switch
        {
            LogicalElementType.Resistor => new LogicalResistor {Name = name},
            LogicalElementType.Capasitor => new LogicalCapasitor {Name = name},
            LogicalElementType.Inductor => new LogicalInductor {Name = name},
            LogicalElementType.Diod => new LogicalDiod {Name = name},
            LogicalElementType.Groud => new LogicalGround {Name = name},
            LogicalElementType.VCC => new LogicalVcc() {Name = name},
            LogicalElementType.Wire => new LogicalWire() {Name = name},
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public T CreateElement<T>(LogicalElementType type, string name) where T : ILogicalElement
    {
        return (T) CreateElement(type, name);
    }

    public ILogicalElement CreateElement(LogicalElementType type, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        
        var newItem = CreateElementInternal(type, name);
        
        if (_registry.Contains(newItem))
        {
            ExceptionHelper.Throw($"Such element has been already created {newItem}");
        }
        
        _registry.Add(newItem);

        return newItem;
    }

    public bool Contains(ILogicalElement element)
    {
        return _registry.Contains(element);
    }
}