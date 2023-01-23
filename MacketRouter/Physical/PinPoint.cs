namespace MacketRouter.Physical;

internal record struct PinPoint : IComparable<PinPoint>
{
    public int X { get; }
    public int Y { get; }
    public BoardSide Side { get; }
    public int BoardId { get; }

    public PinPoint(int x, int y, BoardSide side, int boardId  = 0)
    {
        if (x < 0) throw new ArgumentOutOfRangeException(nameof(x));
        if (y < 0) throw new ArgumentOutOfRangeException(nameof(y));

        X = x;
        Y = y;
        Side = side;
        BoardId = boardId;
    }

    public int CompareTo(PinPoint other)
    {
        var xComparison = X.CompareTo(other.X);
        if (xComparison != 0) return xComparison;
        return Y.CompareTo(other.Y);
    }
}