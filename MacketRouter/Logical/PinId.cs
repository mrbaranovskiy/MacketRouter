namespace MacketRouter.Logical;

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