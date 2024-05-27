using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalGroup
{
    public FinalPoint Point { get; set; }
    public List<object> Values { get; set; }
    public FinalGroup(FinalPoint point, List<object>? values = null)
    {
        Point = point;
        Values = values ?? new List<object>();
    }
}