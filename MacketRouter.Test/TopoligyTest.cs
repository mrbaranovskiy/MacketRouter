using MacketRouter.DataStructures;
using MacketRouter.Logical.LogicalElements;

namespace MacketRouter.Test;

[TestClass]
public class TopoligyTest
{
    [TestMethod]
    public void TryFindTest()
    {
        TopologyBuilder builder = new TopologyBuilder();
        var r1 = builder.BuildElement<LogicalCapasitor>(LogicalElementType.Capasitor, "C1");
        var found = builder.Contains(r1);
        
        Assert.IsTrue(found);
    }
    
    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void CannotCreateDuplicates()
    {
        TopologyBuilder builder = new TopologyBuilder();
        var r1 = builder.BuildElement<LogicalResistor>(LogicalElementType.Resistor);
        var r2 = builder.BuildElement<LogicalResistor>(LogicalElementType.Resistor);
    }

    [TestMethod]
    public void AllConnectedPinsAreConnectedToTheSameHub()
    {
        var builder = new TopologyBuilder();
        var input = builder.BuildElement<LogicalVcc>(LogicalElementType.VCC, "5V+");
        
        var r1 = builder.BuildElement<LogicalResistor>(LogicalElementType.Resistor, "R1");
        var c1 = builder.BuildElement<LogicalCapasitor>(LogicalElementType.Capasitor, "C1");
        var l1 = builder.BuildElement<LogicalInductor>(LogicalElementType.Inductor, "L1");
        var d1 = builder.BuildElement<LogicalDiod>(LogicalElementType.Diod, "D1");
        var gnd = builder.BuildElement<LogicalGround>(LogicalElementType.Groud, "GND1");

        r1.PinB.ConnectTo(c1.PinA);
        l1.PinA.ConnectTo(c1.PinA);

        Assert.IsTrue(r1.PinB.Connection?.ConnectedPins.Count == 3);
        Assert.IsTrue(l1.PinA.Connection?.ConnectedPins.Count == 3);
        Assert.IsTrue(c1.PinA.Connection?.ConnectedPins.Count == 3);
    }

    [TestMethod]
    public void BuildFromSchemeTest()
    {
        string[] scheme =
        {
            "R R1 A1 B1",
            "C C1 B1 B2",
            "L L1 B2 B3",
        };

        var top = new TopologyBuilder();
        top.Build(scheme).ToArray();
    }
}

[TestClass]
public class TraverseTest
{
    [TestMethod]
    public void FindTest()
    {
        string[] scheme =
        {
            
            "VCC 5V+ A1",
            "R R1 A1 B1",
            "C C1 B1 B2",
            "L L1 B1 B3",
            "R R2 A1 B3",
            "GND Gnd B2",
        };

        var top = new TopologyBuilder();
        var elements = top.Build(scheme).ToArray();

        foreach (var d in elements)
        {
            var logicalElement = TopologyTraverse.FindRelative(d, "L1");
        }
    }
}