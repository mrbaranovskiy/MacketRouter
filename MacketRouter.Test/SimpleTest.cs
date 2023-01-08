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
    
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void CannotCreateDuplicates()
    {
        TopologyBuilder builder = new TopologyBuilder();
        var r1 = builder.CreateElement(LogicalElementType.Capasitor, "R1");
        var r2 = builder.CreateElement(LogicalElementType.Capasitor, "R1");
    }

    [TestMethod]
    public void CreateComplex()
    {
        var builder = new TopologyBuilder();
        var input = builder.CreateElement<LogicalVcc>(LogicalElementType.VCC, "5V+");
        
        var r1 = builder.CreateElement<LogicalResistor>(LogicalElementType.Resistor, "R1");
        var c1 = builder.CreateElement<LogicalCapasitor>(LogicalElementType.Capasitor, "C1");
        var l1 = builder.CreateElement<LogicalInductor>(LogicalElementType.Inductor, "L1");
        var d1 = builder.CreateElement<LogicalDiod>(LogicalElementType.Diod, "D1");
        var gnd = builder.CreateElement<LogicalGround>(LogicalElementType.Groud, "GND1");

        r1.PinA.ConnectTo(input.Input);
        r1.PinB.ConnectTo(c1.PinA);
        c1.PinB.ConnectTo(gnd.Gnd);
        l1.PinA.ConnectTo(r1.PinB);
        l1.PinB.ConnectTo(gnd.Gnd);
        d1.Cathode.ConnectTo(r1.PinA);
        d1.Anod.ConnectTo(gnd.Gnd);
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
        
        Assert.IsTrue(r1.PinA.Connections.ConnectedPins.Count == 2);
        Assert.IsTrue(c1.PinB.Connections.ConnectedPins.Count == 2);
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
        
        Assert.IsTrue(r1.PinA.Connections.ConnectedPins.Count == 4);
        
        r1.PinA.DisconnectFrom(c1.PinB);
        
        Assert.IsTrue(r1.PinA.Connections.ConnectedPins.Count == 3);
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
        
        Assert.IsTrue(r1.PinA.Connections.ConnectedPins.Count == 3);
        r1.PinA.DisconnectFrom(c1.PinB, c2.PinB);
        Assert.IsTrue(r1.PinA.Connections.ConnectedPins.Count == 1);
        c3.PinB.DisconnectFrom(r1.PinA);
        Assert.IsTrue(r1.PinA.Connections.ConnectedPins.Count == 0);
    }
    
    [TestMethod]
    public void DisconnectionFromMultiple3()
    {
        var r1 = new LogicalResistor {Name = "R1"};
        var c1 = new LogicalCapasitor {Name = "C1"};
        var c2 = new LogicalCapasitor {Name = "C2"};
        var c3 = new LogicalCapasitor {Name = "C3"};
        
        r1.PinA.ConnectTo(c1.PinB, c2.PinB, c3.PinB);
        
        Assert.IsTrue(r1.PinA.Connections.ConnectedPins.Count == 3);
        Assert.IsTrue(r1.PinB.Connections.ConnectedPins.Count == 0);
    }
}