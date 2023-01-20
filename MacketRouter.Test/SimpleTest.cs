using MacketRouter.DataStructures;
using MacketRouter.Logical.LogicalElements;

namespace MacketRouter.Test;

[TestClass]
public class SimpleTest
{
    [TestMethod]
    public void SimpleConnection1()
    {
        var r1 = new LogicalResistor {Name = "R1"};
        var c1 = new LogicalCapasitor {Name = "C1"};
        r1.PinA.ConnectTo(c1.PinB);
        
        Assert.IsTrue(r1.PinA.Connection?.ConnectedPins.Count == 2);
        Assert.IsTrue(c1.PinB.Connection?.ConnectedPins.Count == 2);
    }

    [TestMethod]
    public void DisconnectionFromMultiple()
    {
        var r1 = new LogicalResistor {Name = "R1"};
        var c1 = new LogicalCapasitor {Name = "C1"};
        var c2 = new LogicalCapasitor {Name = "C2"};
        var c3 = new LogicalCapasitor {Name = "C3"};
        
        r1.PinA.ConnectTo(c1.PinB);
        r1.PinA.ConnectTo(c2.PinB);
        r1.PinA.ConnectTo(c3.PinB);
        
        Assert.IsTrue(r1.PinA.Connection?.ConnectedPins.Count == 4);
        r1.PinA.DisconnectFrom(c1.PinB);
        Assert.IsTrue(r1.PinA.Connection?.ConnectedPins.Count == 3);
    }
    
    [TestMethod]
    public void DisconnectionFromMultiple2()
    {
        var r1 = new LogicalResistor {Name = "R1"};
        var c1 = new LogicalCapasitor {Name = "C1"};
        var c2 = new LogicalCapasitor {Name = "C2"};
        var c3 = new LogicalCapasitor {Name = "C3"};
        
        r1.PinA.ConnectTo(c1.PinB);
        r1.PinA.ConnectTo(c2.PinB);
        r1.PinA.ConnectTo(c3.PinB);
        
        Assert.IsTrue(r1.PinA.Connection?.ConnectedPins.Count == 4);
        r1.PinA.DisconnectFrom(c1.PinB, c2.PinB);
        Assert.IsTrue(r1.PinA.Connection?.ConnectedPins.Count == 2);
    }
    
    [TestMethod]
    public void DisconnectionFromMultiple3()
    {
        var r1 = new LogicalResistor {Name = "R1"};
        var c1 = new LogicalCapasitor {Name = "C1"};
        var c2 = new LogicalCapasitor {Name = "C2"};
        var c3 = new LogicalCapasitor {Name = "C3"};
        
        r1.PinA.ConnectTo(c1.PinB, c2.PinB, c3.PinB);
        Assert.IsTrue(r1.PinA.Connection?.ConnectedPins.Count == 4);
    }
}