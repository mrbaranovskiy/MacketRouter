namespace MacketRouter.Physical;

public interface IPosition
{
    int Horizontal { get; }
    int Vertical { get; }
    int Board { get; }
    BoardSide BoardSide { get; }
}

// should know nothing about the board.

public enum BoardSide
{
    Top, Low
} 

public class Board
{
      
}
