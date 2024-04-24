using GASLanguageProcessor.AST.Expressions;

namespace GASLanguageProcessor.TableType;

public interface ITable<T>
{
    public void Add(string key, T value);
    public T Get(string key);
    public bool Contains(string key);
    public void Remove(string key);
    public void Clear();
}