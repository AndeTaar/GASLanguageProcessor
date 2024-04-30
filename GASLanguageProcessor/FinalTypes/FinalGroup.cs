using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalGroup
{
    public FinalPoint Point { get; set; }
    public Scope Scope { get; set; }

    public FinalGroup(FinalPoint point, Scope scope)
    {
        Point = point;
        Scope = scope;
    }
}