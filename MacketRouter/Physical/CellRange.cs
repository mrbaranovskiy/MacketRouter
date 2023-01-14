namespace MacketRouter.Physical;

public struct BlockRange : IBlockRange 
{
    private int _from;
    private int _to;

    public BlockRange(int from, int to)
    {
        _from = from;
        _to = to;
    }

    public int From
    {
        readonly get => _from;
        set
        {
            if (_from < 0)
                throw new ArgumentOutOfRangeException();
            _from = value;
        }
    }

    public int To
    {
        readonly get => _to;
        set
        {
            if (_to < 0)
                throw new ArgumentOutOfRangeException();
            _to = value;
        }
    }
}