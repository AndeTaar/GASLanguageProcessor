using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.TableType;

public class FunctionType
{
    public GasType ReturnType { get; protected set; }

    public List<GasType> ParameterTypes { get; protected set; }

    public FunctionType(GasType returnType, List<GasType> parameterTypes)
    {
        ReturnType = returnType;
        ParameterTypes = parameterTypes;
    }
}