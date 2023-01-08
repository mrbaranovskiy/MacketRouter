using System.Diagnostics;
using MacketRouter.Logical.LogicalElements;

namespace MacketRouter.Logical
{
    [DebuggerDisplay("Def: {Description}")]
    class PinDescription
    {
        private AbstractLogicalElement? _parent;

        public enum PinPurpose
        {
            A, B, C,
            Base, Collector, Emmiter, GND, Vcc, Vdd, Pin, Gate, Source, Drain, Anod, Cathode
        }

        /// <summary> Gets the text description. </summary>
        public string Description => $"{_parent?.Name ?? "None"}({Purpose})";  
        /// <summary>
        /// Gets the pin numbering. Port and some id.
        /// </summary>
        public PinId? Id { get; set; }
        /// <summary>
        /// Shows the ping purpose. Can be the transistop base or the
        /// identification of the duo poles. Some stuff to determine who is who it future.
        /// </summary>
        public required PinPurpose Purpose { get; set; }

        public void SetDisplayedParent(AbstractLogicalElement parent)
        {
            _parent = parent;
        }
    }

    public class PinId
    {
        private readonly short _number;
        private readonly byte _port;

        public PinId(short number, byte port)
        {
            _number = number;
            _port = port;
        }
    }
}