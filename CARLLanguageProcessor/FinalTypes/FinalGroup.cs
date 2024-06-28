using CARLLanguageProcessor.TableType;

namespace CARLLanguageProcessor.FinalTypes;

public class FinalGroup : FinalType
{
    public FinalGroup(FinalPoint point, Store store)
    {
        Point = point;
        Store = store;
    }

    public FinalPoint Point { get; set; }
    public Store Store { get; set; }
}
