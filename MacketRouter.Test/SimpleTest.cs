using MacketRouter.Logical.LogicalElements;

namespace MacketRouter.Test;

[TestClass]
public class TopoligyTest
{
    [TestMethod]
    public void TryFindTest()
    {
        TopologyBuilder builder = new TopologyBuilder();
        var r1 = builder.CreateElement(LogicalElementType.Capasitor, "R1");
        var found = builder.Contains(r1);
        
        Assert.IsTrue(found);
    }
}

[TestClass]
public class SimpleTest
{
    [TestMethod]
    public void SimpleConnection1()
    {
        var r1 = new LogicalResistor {Name = "R1"};
        var c1 = new LogicalCapasitor {Name = "C1"};
        r1.PinA.ConnectTo(c1.PinB);
        
        Assert.IsTrue(r1.PinA.Connections.Count == 1);
        Assert.IsTrue(c1.PinB.Connections.Count == 1);
    }

    [TestMethod]
    public void EquealityTest()
    {
        var c1 = new LogicalCapasitor {Name = "C1"};
        var c2 = new LogicalCapasitor {Name = "C1"};
        
        var b = c1 == c2;
        
        Assert.IsTrue(b);
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
        
        Assert.IsTrue(r1.PinA.Connections.Count == 3);
        
        r1.PinA.DisconnectFrom(c1.PinB);
        
        Assert.IsTrue(r1.PinA.Connections.Count == 2);
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
        
        Assert.IsTrue(r1.PinA.Connections.Count == 3);
        r1.PinA.DisconnectFrom(c1.PinB, c2.PinB);
        Assert.IsTrue(r1.PinA.Connections.Count == 1);
        c3.PinB.DisconnectFrom(r1.PinA);
        Assert.IsTrue(r1.PinA.Connections.Count == 0);
    }
    
    [TestMethod]
    public void DisconnectionFromMultiple3()
    {
        var r1 = new LogicalResistor {Name = "R1"};
        var c1 = new LogicalCapasitor {Name = "C1"};
        var c2 = new LogicalCapasitor {Name = "C2"};
        var c3 = new LogicalCapasitor {Name = "C3"};
        
        r1.PinA.ConnectTo(c1.PinB, c2.PinB, c3.PinB);
        
        Assert.IsTrue(r1.PinA.Connections.Count == 3);
        Assert.IsTrue(r1.PinB.Connections.Count == 0);
    }
}