using System.Collections.Immutable;

namespace MacketRouter.Physical;

public interface IElement
{
    IList<IPosition> GetConnectors();
}

public interface IPosition
{
    int Horizontal { get; }
    int Vertical { get; }
    int Board { get; }
    Board.Side Side { get; }
}

interface IBlock
{
    public BlockRange Range { get; }
    public void ExtendBlock(BlockRange index);
    public static int MaxBlockHeight { get; }
    public static int MaxBlockWidth { get; }
    public ImmutableHashSet<PhysicalPin> Pins { get; }
}

interface ISection
{
    IList<StandardBlock> Blocks { get; }
}

public interface IBlockRange
{
    public int From { get; }
    public int To { get; }
}


class StandardBlock : IBlock
{
    private readonly Board _board;
    private HashSet<PhysicalPin> _pinsEncluded;
    private ImmutableHashSet<PhysicalPin> _immutable = ImmutableHashSet<PhysicalPin>.Empty;

    public StandardBlock(BlockRange range, Board board)
    {
        _board = board;
        Range = range;
        _pinsEncluded = new HashSet<PhysicalPin>();
        ExtendBlock(range);
    }

    public BlockRange Range { get; }
    
    public void ExtendBlock(BlockRange index)
    {
        if (index.To > MaxBlockWidth)
            throw new ArgumentOutOfRangeException($"The block extent should be in range ({MaxBlockHeight}, {MaxBlockWidth})");

        // fast and dirty.
        for (int i = index.From; i <= index.To ; i++)
        {
            for (int j = 0; j < MaxBlockHeight; j++)
            {
                var newPin = new PhysicalPin(i, j);
                
                if (!_pinsEncluded.Contains(newPin)) 
                    _pinsEncluded.Add(newPin);
            }
        }

        _immutable = _pinsEncluded.ToImmutableHashSet();
    }

    public int MaxBlockHeight => 5;
    public int MaxBlockWidth => 60;
    public ImmutableHashSet<PhysicalPin> Pins => _immutable;
}

public class Board
{
    public enum Side
    {
        Top, Low
    }   
}
