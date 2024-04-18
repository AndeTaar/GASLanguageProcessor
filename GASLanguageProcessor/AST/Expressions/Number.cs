namespace GASLanguageProcessor.AST.Expressions;

public class Number : Expression
{
    public float Value { get; protected set; }

    public Number(float value)
    {
        Value = value;
    }

    public override AstNode Accept(IAstVisitor visitor)
    {
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}