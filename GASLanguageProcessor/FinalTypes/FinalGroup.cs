using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.FinalTypes;

public class FinalGroup : FinalType
{
    public FinalGroup(FinalPoint point, VarEnv envV)
    {
        Point = point;
        EnvV = envV;
    }

    public FinalPoint Point { get; set; }
    public VarEnv EnvV { get; set; }
}