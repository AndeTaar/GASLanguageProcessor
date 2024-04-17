namespace GASLanguageProcessor.AST.Expressions;

public class Variable : Expression
{
    public string Name { get; protected set; }

    public Variable(string name)
    {
        Name = name;
    }
}