using MacketRouter.Physical;
using MacketRouter.Utilities;

namespace MacketRouter.Test;

[TestClass]
public class BlockTest
{
    [TestMethod]
    public void StandardBlockCount()
    {
        var board = new Board();
        IBlock block = new StandardBlock(new BlockRange(0, 0), board);

        Assert.IsTrue(block.Pins.Count == 5);
    }

    [TestMethod]
    public void ExtendRange()
    {
        var board = new Board();
        IBlock block = new StandardBlock(new BlockRange(0, 0), board);
        
        block.ExtendBlock(new BlockRange(0,1));
        Assert.IsTrue(block.Pins.Count == 10);  
    }
}

[TestClass]
public class ElementsRectTest
{
    [TestMethod]
    public void RectsIntersectionTest()
    {
    }
}