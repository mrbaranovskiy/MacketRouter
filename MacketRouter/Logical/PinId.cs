namespace MacketRouter.Logical;

public struct PinId
{
    public short Number { get; }
    public byte Port { get; }

    public PinId(short number, byte port)
    {
        Number = number;
        Port = port;
    }
}