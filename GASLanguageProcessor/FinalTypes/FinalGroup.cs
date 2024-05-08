using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalGroup
{
    public FinalPoint Point { get; set; }
    public List<object> Values { get; set; }
    public Scope Scope { get; set; }
    public FinalGroup(FinalPoint point, Scope scope, List<object>? values = null)
    {
        Point = point;
        Scope = scope;
        Values = values ?? new List<object>();
    }
}