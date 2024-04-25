using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.TableType;

public interface ITable<T>
{
    public void Bind(string key, T value);

    public T LookUp(string key);

    public FunctionTable EnterScope();

    public FunctionTable ExitScope();
}