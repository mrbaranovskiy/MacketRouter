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

public class Board
{
    public enum Side
    {
        Top, Low
    }   
}

internal class Element : IElement
{
    public IList<IPosition> GetConnectors()
    {
        throw new NotImplementedException();
    }
}