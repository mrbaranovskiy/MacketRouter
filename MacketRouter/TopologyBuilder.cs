using MacketRouter.DataStructures;
using MacketRouter.Logical;
using MacketRouter.Logical.LogicalElements;
using MacketRouter.Utilities;

namespace MacketRouter;

internal sealed class TopologyBuilder
{
    private readonly HashSet<ILogicalElement> _registry;
    // Used for counting the logical elements.
    private readonly Dictionary<LogicalElementType, int> _counters;

    public TopologyBuilder()
    {
        _registry = new HashSet<ILogicalElement>();
        _counters = new Dictionary<LogicalElementType, int>();
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
            LogicalElementType.Transistor => new LogicalTransistor() {Name = name},
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public T CreateElement<T>(LogicalElementType type, string name) where T : ILogicalElement
    {
        AutoCount(type);
        return (T) CreateElement(type, name);
    }

    private Dictionary<string, AbstractLogicalPin> _connectionsNetwork = new Dictionary<string, AbstractLogicalPin>();
    
    public T CreateElement<T>(LogicalElementType type, string name, params string[] conns) where T : ILogicalElement
    {
        AutoCount(type);
        var elem = (T) CreateElement(type, name);
        AddToNetwork(elem, conns);
        return elem;
    }

    private void AddToNetwork<T>(T elem, string[] connectionLabels) where T : ILogicalElement
    {
        if (elem.Pins.Count != connectionLabels.Length) throw new Exception("Data is inconsistent.An element has beeb created incorrectly!!!");
        
        foreach ((string label, AbstractLogicalPin pin) zip in connectionLabels.Zip(elem.Pins))
        {
            if (_connectionsNetwork.ContainsKey(zip.label))
            {
                var targetPin = _connectionsNetwork[zip.label];
                zip.pin.ConnectTo(targetPin);
            }
            else
            {
                _connectionsNetwork[zip.label] = zip.pin;
            }
        }
    }

    public bool Contains(ILogicalElement element)
    {
        return _registry.Contains(element);
    }

    public T CreateElement<T>(LogicalElementType type) where T : ILogicalElement
    {
        AutoCount(type);
        return (T) CreateElement(type, AutomaticName(type));
    }

    private ILogicalElement CreateElement(LogicalElementType type, string name)
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

    public void Build(string[] scheme)
    {
        if (scheme == null) throw new ArgumentNullException(nameof(scheme));
        if (scheme.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(scheme));

        foreach (var element in scheme)
        {
            ParseLogicalElement(element);
        }
    }

    private ILogicalElement ParseLogicalElement(string data)
    {
        var split = data.Split(' ');
        return split switch
        {
            ["R", string name, string a, string b] 
                => this.CreateElement<LogicalResistor>(LogicalElementType.Resistor, name, a, b),
            ["C", string name, string a, string b]
                => this.CreateElement<LogicalCapasitor>(LogicalElementType.Capasitor, name, a, b),
            ["L", string name, string a, string b]
                => this.CreateElement<LogicalInductor>(LogicalElementType.Inductor, name, a, b),
            ["T", string name, string e, string b, string c]
                => this.CreateElement<LogicalInductor>(LogicalElementType.Transistor, name, e, b, c),
            ["GND", string name, string gnd] 
                => this.CreateElement<LogicalResistor>(LogicalElementType.Groud, name, gnd),
            ["VCC", string name, string vcc] 
                => this.CreateElement<LogicalResistor>(LogicalElementType.VCC, name, vcc),
            _ => throw new ArgumentException("Cannot parse input pattern")
        };
    }

    private void AutoCount(LogicalElementType type)
    {
        if (!_counters.ContainsKey(type))
            _counters[type] = 1;
        else _counters[type] += 1;
    }

    private string AutomaticName(LogicalElementType type) => type switch
    {
        LogicalElementType.Capasitor => $"C{_counters[type]}",
        LogicalElementType.Resistor => $"R{_counters[type]}",
        LogicalElementType.Diod => $"D{_counters[type]}",
        LogicalElementType.Groud => $"GND{_counters[type]}",
        LogicalElementType.Inductor => $"L{_counters[type]}",
        LogicalElementType.Wire => $"Wire{_counters[type]}",
        LogicalElementType.VCC => $"Vcc{_counters[type]}",
        _ => throw new ArgumentException("Cannot parse")
        
    };

}