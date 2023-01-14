using MacketRouter.DataStructures;

namespace MacketRouter.DataStructures;

public static class ComponentsLibrary
{
    public enum LibraryElement
    { 
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
        TransitorDIP308
    }
    
    //todo: put all this stuff to the configuration file.
    
    private static Dictionary<LibraryElement, ElementSize> SizeMap = new Dictionary<LibraryElement, ElementSize>()
    {
        {LibraryElement.ResistorAXIAL03, new ElementSize(1, 4)}, 
        {LibraryElement.ResistorAXIAL1, new ElementSize(2, 4)}, 
        {LibraryElement.CapasitorElectrolitiс, new ElementSize(1, 3)}, 
        {LibraryElement.Capacitor, new ElementSize(1, 4)},
        {LibraryElement.Diod, new ElementSize(1, 4)},
        {LibraryElement.TransitorDIP308, new ElementSize(1, 3)},
    };

    public static ElementSize MapSize(LibraryElement element) => element switch
    {
        LibraryElement.Capacitor => SizeMap[LibraryElement.Capacitor],
        LibraryElement.TransitorDIP308 => SizeMap[LibraryElement.TransitorDIP308],
        LibraryElement.Diod => SizeMap[LibraryElement.Diod],
        LibraryElement.ResistorAXIAL03 => SizeMap[LibraryElement.ResistorAXIAL03],
        LibraryElement.CapasitorElectrolitiс => SizeMap[LibraryElement.CapasitorElectrolitiс],
        LibraryElement.ResistorAXIAL1 => SizeMap[LibraryElement.ResistorAXIAL1],
        
        _ => throw new ArgumentOutOfRangeException("This element has no mapping in library")
    };
}