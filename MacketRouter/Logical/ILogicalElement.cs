using System.Collections.ObjectModel;

namespace MacketRouter.Logical
{
    namespace LogicalElements
    {
        /// <summary> Represents the logical radio element. </summary>
        interface ILogicalElement : IEquatable<ILogicalElement>, IComparable<ILogicalElement>
        {
            IReadOnlyList<AbstractLogicalPin> Pins { get; }
            string Name { get; set; }
        }

        abstract class AbstractLogicalElement : ILogicalElement, IComparable<AbstractLogicalElement>
        {
            protected AbstractLogicalElement(List<LogicalPin> defaultPins)
            {
                Pins = defaultPins;

                foreach (var pin in defaultPins)
                    pin.Description.SetDisplayedParent(this);
            }


            public IReadOnlyList<AbstractLogicalPin> Pins { get; }
            public required string Name { get; set; }

            public bool Equals(ILogicalElement x, ILogicalElement y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Pins.Equals(y.Pins);
            }

            public int GetHashCode(ILogicalElement obj)
            {
                return obj.Pins.GetHashCode();
            }

            public int CompareTo(AbstractLogicalElement? other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return string.Compare(Name, other.Name, StringComparison.Ordinal);
            }

            public int CompareTo(ILogicalElement? other)
            {
                return string.Compare(Name, other?.Name, StringComparison.Ordinal);
            }

            protected bool Equals(AbstractLogicalElement other)
            {
                return Name == other.Name;
            }

            public bool Equals(ILogicalElement? other)
            {
                return Name == other?.Name;
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((AbstractLogicalElement) obj);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }
        }

        abstract class LogicalDuoPole : AbstractLogicalElement
        {
            protected LogicalDuoPole() : base(
                new List<LogicalPin>
                {
                    new LogicalPin
                    {
                        Description = new PinDescription
                        {
                            Id = new PinId(0, 0),
                            Purpose = PinDescription.PinPurpose.A
                        }
                    },
                    new LogicalPin
                    {
                        Description = new PinDescription()
                        {
                            Id = new PinId(1, 0),
                            Purpose = PinDescription.PinPurpose.B,
                        }
                    }
                })
            {
            }

            public AbstractLogicalPin PinA => this.Pins[0];
            public AbstractLogicalPin PinB => this.Pins[1];
        }

        class LogicalResistor : LogicalDuoPole
        {
        }

        class LogicalCapasitor : LogicalDuoPole
        {
        }

        class LogicalInductor : LogicalDuoPole
        {
        }

        class LogicalWire : LogicalDuoPole
        {
        }

        class LogicalDiod : AbstractLogicalElement
        {
            public LogicalDiod() : base(
                new List<LogicalPin>
                {
                    new LogicalPin
                    {
                        Description = new PinDescription
                        {
                            Id = new PinId(0, 0),
                            Purpose = PinDescription.PinPurpose.Anod
                        }
                    },
                    new LogicalPin
                    {
                        Description = new PinDescription
                        {
                            Id = new PinId(1, 0),
                            Purpose = PinDescription.PinPurpose.Cathode
                        }
                    }
                }
            )
            {
            }
            
            public AbstractLogicalPin Anod => this.Pins[0];
            public AbstractLogicalPin Cathode => this.Pins[1];
            
        }

        class LogicalGround : AbstractLogicalElement
        {
            public LogicalGround() : base(new List<LogicalPin>()
            {
                new LogicalPin()
                {
                    Description = new PinDescription()
                    {
                        Id = new PinId(0, 0),
                        Purpose = PinDescription.PinPurpose.GND
                    }
                }
            })
            {
            }
            
            public AbstractLogicalPin Gnd => this.Pins[0];
        }
        
        class LogicalVcc : AbstractLogicalElement
        {
            public LogicalVcc() : base(new List<LogicalPin>()
            {
                new LogicalPin()
                {
                    Description = new PinDescription()
                    {
                        Id = new PinId(0, 0),
                        Purpose = PinDescription.PinPurpose.Vcc
                    }
                }
            })
            {
            }
            
            public AbstractLogicalPin Input => this.Pins[0];
        }

        class LogicalTransistor : AbstractLogicalElement
        {
            public LogicalTransistor() : base(new List<LogicalPin>
                {
                    new LogicalPin
                    {
                        Description = new PinDescription
                        {
                            Id = new PinId(0, 0),
                            Purpose = PinDescription.PinPurpose.Emmiter
                        }
                    },
                    new LogicalPin
                    {
                        Description = new PinDescription
                        {
                            Id = new PinId(1, 0),
                            Purpose = PinDescription.PinPurpose.Base
                        }
                    },
                    new LogicalPin
                    {
                        Description = new PinDescription
                        {
                            Id = new PinId(2, 0),
                            Purpose = PinDescription.PinPurpose.Collector
                        }
                    }
                }
            )
            {
            }
        }
    }
}