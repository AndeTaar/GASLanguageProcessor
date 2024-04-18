namespace GASLanguageProcessor.AST.Statements;

public class Skip : Statement
{
    public Skip()
    {
    }


    public override AstNode Accept(IAstVisitor visitor, string indent)
    {
        Console.WriteLine(indent + this.GetType().Name);
        return this;
    }
}