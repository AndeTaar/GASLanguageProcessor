namespace GASLanguageProcessor.AST.Statements;

public class Skip : Statement
{
    public Skip()
    {
    }

    public override AstNode Accept(IAstVisitor visitor)
    {
        Console.WriteLine(this.GetType().Name);
        return this;
    }
}