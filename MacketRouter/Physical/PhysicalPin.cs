namespace MacketRouter.Physical;

internal record struct PhysicalPin : IComparable<PhysicalPin>
{
    public int X { get; }
    public int Y { get; }

    public PhysicalPin(int x, int y)
    {
        if (x < 0) throw new ArgumentOutOfRangeException(nameof(x));
        if (y < 0) throw new ArgumentOutOfRangeException(nameof(y));

        X = x;
        Y = y;
    }

    public int CompareTo(PhysicalPin other)
    {
        var xComparison = X.CompareTo(other.X);
        if (xComparison != 0) return xComparison;
        return Y.CompareTo(other.Y);
    }
}