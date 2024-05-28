using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalGroup
{
    public FinalPoint Point { get; set; }
    public VarEnv EnvV { get; set; }

    public FinalGroup(FinalPoint point, VarEnv envV)
    {
        Point = point;
        EnvV = envV;
    }
}