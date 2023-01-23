#region Information

// File IElementPin.cs has been created by: Dmytro Baranovskyi at: 2023 01 23

// Description:  Describes the physical electrical element pin on the board. 
// Element name is the name of the concrete element in the Circuit.
// Definition is the literal definition of the pin connection.

// Example: 
// Resistor R1 has to pins A and B. So the pinouts will have a name smth like R1:A and R1:B
// In more complicated scenarioes for controllers (for ex) the pins may have ports and port numbers, ake
// M1:A0_0, M1:VCC0_1 etc.

#endregion

using System.Diagnostics;
using MacketRouter.Utilities;

namespace MacketRouter.Physical;

[DebuggerDisplay("Pin: {ElementName}:{PinDefinition}")]
internal record struct ElementPin
{
    public string ElementName { get; }
    public string PinDefinition { get; }

    public ElementPin(string pinFullDefinition) : this()
    {
        (string a, string b) = Deconstruct(pinFullDefinition);

        ElementName = a;
        PinDefinition = b;
    }

    public ElementPin(string name, string definition)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
        if (string.IsNullOrEmpty(definition))
            throw new ArgumentException("Value cannot be null or empty.", nameof(definition));

        ElementName = name;
        PinDefinition = definition;
    }

    public static implicit operator ElementPin(string fullDefinition)
    {
        (string a, string b) = Deconstruct(fullDefinition);
        return new ElementPin(a, b);
    }

    private static (string, string) Deconstruct(string definition)
    {
        var split = definition.Split(":");
        
        if(split.Length != 2) 
            ExceptionHelper.Throw("Cannot parse the definition");

        return (split[0], split[1]);
    }
}