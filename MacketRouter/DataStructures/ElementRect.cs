using System.Drawing;

namespace MacketRouter.DataStructures;

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