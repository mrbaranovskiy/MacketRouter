namespace MacketRouter.DataStructures;

public static class ComponentsLibrary
{
    public enum FrameType
    {
        Generic,
        ResistorAXIAL04,
        ResistorAXIAL03, //7.6 mm
        ResistorAXIAL05,
        ResistorAXIAL06,
        ResistorAXIAL08,
        ResistorAXIAL1, //25mm
        CapasitorElectrolitiс,
        CapacitorCeramic,
        Capacitor, 
        Diod,
        TransitorDIP308,
        Ground
    }
    
    //todo: put all this stuff to the configuration file.
    
    private static Dictionary<FrameType, ElementSize> SizeMap = new Dictionary<FrameType, ElementSize>()
    {
        {FrameType.ResistorAXIAL03, new ElementSize(1, 4)}, 
        {FrameType.ResistorAXIAL1, new ElementSize(2, 4)}, 
        {FrameType.CapasitorElectrolitiс, new ElementSize(1, 3)}, 
        {FrameType.Capacitor, new ElementSize(1, 4)},
        {FrameType.Diod, new ElementSize(1, 4)},
        {FrameType.TransitorDIP308, new ElementSize(1, 3)},
        {FrameType.Generic, new ElementSize(1, 4)},
        {FrameType.Ground, new ElementSize(1, 1)},
    };

    public static ElementSize MapSize(FrameType element) => element switch
    {
        FrameType.Capacitor => SizeMap[FrameType.Capacitor],
        FrameType.TransitorDIP308 => SizeMap[FrameType.TransitorDIP308],
        FrameType.Diod => SizeMap[FrameType.Diod],
        FrameType.ResistorAXIAL03 => SizeMap[FrameType.ResistorAXIAL03],
        FrameType.CapasitorElectrolitiс => SizeMap[FrameType.CapasitorElectrolitiс],
        FrameType.ResistorAXIAL1 => SizeMap[FrameType.ResistorAXIAL1],
        
        _ => throw new ArgumentOutOfRangeException("This element has no mapping in library")
    };
}