using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class List : Term
{
    public Identifier Identifier { get; protected set; }
    public GasType Type { get; set; }
    public List<Expression> Expressions { get; protected set; }
    
    public List(Identifier identifier, List<Expression> expressions, GasType type = GasType.Null)
    {
        Identifier = identifier;
        Type = type;
        Expressions = expressions;
    }

    public override T Accept<T>(IAstVisitor<T> visitor)
    {
        return visitor.VisitListDeclaration(this);
    }
}
