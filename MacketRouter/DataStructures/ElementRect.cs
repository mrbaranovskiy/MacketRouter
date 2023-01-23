using System.Drawing;
using MacketRouter.Physical;

namespace MacketRouter.DataStructures;

/// <summary>
/// Upper left corner of the component
/// </summary>
public record struct ElementPosition
{
    public int X { get; }
    public int Y { get; }

    public ElementPosition(int x, int y)
    {
        if (y < 0 || y > IHub.MaxBlockHeight ) throw new ArgumentOutOfRangeException(nameof(y));
        if (x < 0 || x > IHub.MaxBlockWidth  ) throw new ArgumentOutOfRangeException(nameof(x));
        
        X = x;
        Y = y;
    }

    public static implicit operator ElementPosition((int x, int y) tup) => new ElementPosition(tup.x, tup.y);
}

public record struct ElementRect
{
    public int X { get; }
    public int Y { get; }
    public int Height { get; }
    public int Width { get; }

    public Rectangle Rect { get; }

    public ElementRect(int x, int y, int width, int height)
    {
        if (width < 0) throw new ArgumentOutOfRangeException(nameof(width));
        if (height < 0) throw new ArgumentOutOfRangeException(nameof(height));
        if (y < 0) throw new ArgumentOutOfRangeException(nameof(y));
        if (x < 0) throw new ArgumentOutOfRangeException(nameof(x));

        Rect = new Rectangle(x, y, width, height);
    }
    /// <summary> Shows if the elements intersects with other element. </summary>
    public bool Intersects(ElementRect element) => Rect.IntersectsWith(element.Rect);
}