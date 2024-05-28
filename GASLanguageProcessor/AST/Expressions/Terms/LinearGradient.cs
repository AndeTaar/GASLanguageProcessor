using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor.AST.Expressions.Terms;

public class LinearGradient: Term
{
    public Expression Rotation { get; protected set; }
    public Expression Alpha { get; protected set; }
    public Expression ColorList { get; protected set; }
    public Expression PercentagesList { get; protected set; }

    public LinearGradient(Expression rotation, Expression alpha, Expression colorList, Expression percentagesList)
    {
        Rotation = rotation;
        Alpha = alpha;
        ColorList = colorList;
        PercentagesList = percentagesList;
    }

    public override T Accept<T>(IAstVisitor<T> visitor, Scope scope)
    {
        return visitor.VisitLinearGradient(this, scope);
    }
}
