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

        public string PinReadableDefinition() => Id is {} ? $"{Description}{Id.Value.Number}_{Id.Value.Port}" : Description;

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
}