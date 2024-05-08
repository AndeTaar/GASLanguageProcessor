using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;

namespace Tests.Frontend.ToAstVisitorTests;

public class VisitCollectionDeclaration
{
    [Fact]
    public void PassVisitCollectionDeclarationListWithoutExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "list<circle> c;");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var collectionDeclaration = (CollectionDeclaration) compound.Statement2;
        Assert.NotNull(collectionDeclaration);
        Assert.NotNull(canvas);
        Assert.Equal("c", collectionDeclaration.Identifier.Name);
    }

    [Fact]
    public void PassVisitCollectionDeclarationGroupWithoutExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "group g;");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var collectionDeclaration = (CollectionDeclaration) compound.Statement2;
        Assert.NotNull(collectionDeclaration);
        Assert.NotNull(canvas);
        Assert.Equal("g", collectionDeclaration.Identifier.Name);
    }

    [Fact]
    public void PassVisitCollectionDeclarationListWithExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "list<point> points = List{Point(10,10), Point(20,20), Point(30,30)};");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var collectionDeclaration = (CollectionDeclaration) compound.Statement2;
        Assert.NotNull(collectionDeclaration);
        Assert.NotNull(canvas);
        Assert.Equal("points", collectionDeclaration.Identifier.Name);
        Assert.IsType<List>(collectionDeclaration.Expression);
        var expression = (List) collectionDeclaration.Expression;
        Assert.Equal(3, expression.Expressions.Count);
    }

    [Fact]
    public void PassVisitCollectionDeclarationGroupWithExpression()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "group g = Group(Point(10,10),{" +
            "   circle c = Circle(x, 10, 20);" +
            "   circle c1 = Circle(y, 20, 30);" +
            "});");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var collectionDeclaration = (CollectionDeclaration) compound.Statement2;
        Assert.NotNull(collectionDeclaration);
        Assert.NotNull(canvas);
        Assert.Equal("g", collectionDeclaration.Identifier.Name);
        Assert.IsType<Group>(collectionDeclaration.Expression);
        var group = (Group) collectionDeclaration.Expression;
        Assert.IsType<Compound>(group.Statements);
        var statements = (Compound) group.Statements;
        Assert.IsType<Declaration>(statements.Statement1);
        Assert.IsType<Declaration>(statements.Statement2);
        var declaration1 = (Declaration) statements.Statement1;
        var declaration2 = (Declaration) statements.Statement2;
        Assert.Equal("c", declaration1.Identifier.Name);
        Assert.Equal("c1", declaration2.Identifier.Name);
    }

    [Fact]
    public void PassVisitCollectionDeclarationGroupWithChildGroup()
    {
        var ast = SharedTesting.GetAst(
            "canvas(250, 250, Color(255, 255, 255, 1));" +
            "group g = Group(Point(10,10),{" +
            "   group g1 = Group(Point(20,20),{" +
            "       circle c = Circle(x, 10, 20);" +
            "       circle c1 = Circle(y, 20, 30);" +
            "   });" +
            "});");
        Assert.NotNull(ast);
        Assert.IsType<Compound>(ast);
        var compound = (Compound) ast;
        Assert.IsAssignableFrom<Statement>(compound.Statement1);
        var canvas = (Canvas) compound.Statement1;
        Assert.IsAssignableFrom<Statement>(compound.Statement2);
        var collectionDeclaration = (CollectionDeclaration) compound.Statement2;
        Assert.NotNull(collectionDeclaration);
        Assert.NotNull(canvas);
        Assert.Equal("g", collectionDeclaration.Identifier.Name);
        Assert.IsType<Group>(collectionDeclaration.Expression);
        var group = (Group) collectionDeclaration.Expression;
        Assert.IsType<CollectionDeclaration>(group.Statements);
        var statements = (CollectionDeclaration) group.Statements;
        Assert.Equal("g1", statements.Identifier.Name);
        Assert.IsType<Group>(statements.Expression);
        var innerGroup = (Group) statements.Expression;
        Assert.IsType<Compound>(innerGroup.Statements);
        var innerStatements = (Compound) innerGroup.Statements;
        Assert.IsType<Declaration>(innerStatements.Statement1);
        Assert.IsType<Declaration>(innerStatements.Statement2);
        var declaration1 = (Declaration) innerStatements.Statement1;
        var declaration2 = (Declaration) innerStatements.Statement2;
        Assert.Equal("c", declaration1.Identifier.Name);
        Assert.Equal("c1", declaration2.Identifier.Name);
    }
}