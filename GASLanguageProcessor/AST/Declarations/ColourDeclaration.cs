using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Terms;

namespace GASLanguageProcessor.AST.Declarations;

public class ColourDeclaration : AstNode
{
    public Variable Identifier { get; protected set; }
    public ColourTerm Color { get; protected set; }
    
    public ColourDeclaration(Variable identifier, ColourTerm color)
    {
        Identifier = identifier;
        Color = color;
    }
}