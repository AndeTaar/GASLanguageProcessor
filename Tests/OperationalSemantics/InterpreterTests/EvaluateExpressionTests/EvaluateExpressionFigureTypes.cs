using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class EvaluateExpressionFigureTypes
{   
    [Fact]
    public void PassEvaluateExpressionColor() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (150, 150, Color(255, 255, 255, 1));" +
                                        "color notYellow = Color(184,62,17,0.65);") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("notYellow", new Variable("notYellow", GasType.Color));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalColor;
        
        Assert.NotNull(result);
        Assert.IsType<FinalColor>(result);
        Assert.Equal(184, result.Red);
        Assert.Equal(62, result.Green);
        Assert.Equal(17, result.Blue);
        Assert.Equal(0.65, result.Alpha, 2); //Precision of 2 decimal places added due to floating point
    }
    
    [Fact]
    public void PassEvaluateExpressionPoint() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst("canvas (150, 150, Color(255, 255, 255, 1)); point one = Point(30,50);") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("one", new Variable("one", GasType.Point));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalPoint;
        
        Assert.NotNull(result);
        Assert.IsType<FinalPoint>(result);
        Assert.Equal(30, result.X);
        Assert.Equal(50, result.Y);
    }
    
    [Fact]
    public void PassEvaluateExpressionSqaure() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst(
            "canvas (150, 150, Color(255, 255, 255, 1)); " +
            "square one = Square(Point(10,2), 40, 4, Color(255,0,255,1), Color(255,255,0,1));") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("one", new Variable("one", GasType.Square));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalSquare;
        
        Assert.NotNull(result);
        Assert.IsType<FinalSquare>(result);
        Assert.IsType<FinalPoint>(result.TopLeft);
        Assert.Equal(40, result.Length);
        Assert.Equal(4, result.Stroke);
        Assert.IsType<FinalColor>(result.FillColor);
        Assert.IsType<FinalColor>(result.StrokeColor);
    }
    
    [Fact]
    public void PassEvaluateExpressionEllipse() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst(
            "canvas (150, 150, Color(255, 255, 255, 1)); " +
            "ellipse one = Ellipse(Point(10,2), 5, 10, Color(255,0,255,1), Color(255,255,0,1), 4);") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("one", new Variable("one", GasType.Square));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalEllipse;
        
        Assert.NotNull(result);
        Assert.IsType<FinalEllipse>(result);
        Assert.IsType<FinalPoint>(result.Center);
        Assert.Equal(5, result.RadiusX);
        Assert.Equal(10, result.RadiusY);
        Assert.IsType<FinalColor>(result.Color);
        Assert.IsType<FinalColor>(result.BorderColor);
        Assert.Equal(4, result.BorderWidth);
    }
    
    [Fact]
    public void PassEvaluateExpressionText() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst(
            "canvas (150, 150, Color(255, 255, 255, 1)); " +
            "text one = Text(\"Hello World\", Point(67,37), \"Arial\", 24, Color(0,255,255,1);") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("one", new Variable("one", GasType.Text));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalText;
        
        Assert.NotNull(result);
        Assert.IsType<FinalText>(result);
        Assert.Equal("Hello World", result.Text);
        Assert.IsType<FinalPoint>(result.Position);
        Assert.Equal("Arial", result.Font);
        Assert.Equal(24, result.FontSize);
        Assert.IsType<FinalColor>(result.TextColor);
    }
    
    [Fact]
    public void PassEvaluateExpressionCircle() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst(
            "canvas (150, 150, Color(255, 255, 255, 1)); " +
            "circle one = Circle(Point(10,2), 5, 7, Color(255,255,0,1), Color(175,6,135,1);") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("one", new Variable("one", GasType.Circle));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalCircle;
        
        Assert.NotNull(result);
        Assert.IsType<FinalCircle>(result);
        Assert.IsType<FinalPoint>(result.Center);
        Assert.Equal(5, result.Radius);
        Assert.Equal(7, result.Stroke);
        Assert.IsType<FinalColor>(result.FillColor);
        Assert.IsType<FinalColor>(result.StrokeColor);
    }
    
    [Fact]
    public void PassEvaluateExpressionRectangle() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst(
            "canvas (150, 150, Color(255, 255, 255, 1)); " +
            "rectangle one = Rectangle(Point(5,2), Point(10,4), 9, Color(255,255,0,1), Color(175,6,135,1);") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("one", new Variable("one", GasType.Rectangle));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalRectangle;
        
        Assert.NotNull(result);
        Assert.IsType<FinalRectangle>(result);
        Assert.IsType<FinalPoint>(result.TopLeft);
        Assert.IsType<FinalPoint>(result.BottomRight);
        Assert.Equal(9, result.Stroke);
        Assert.IsType<FinalColor>(result.FillColor);
        Assert.IsType<FinalColor>(result.StrokeColor);
        Assert.Equal(5, result.Width);
        Assert.Equal(2, result.Height);
    }
    
    [Fact]
    public void PassEvaluateExpressionLine() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst(
            "canvas (150, 150, Color(255, 255, 255, 1)); " +
            "line one = Line(10, 20, 4, Color(64,29,11,1);") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("one", new Variable("one", GasType.Line));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalLine;
        
        Assert.NotNull(result);
        Assert.IsType<FinalLine>(result);
        Assert.IsType<FinalPoint>(result.Start);
        Assert.Equal(0, result.Start.X);    //Maybe assert the math instead of the resulting value,
        Assert.Equal(10, result.Start.Y);   //for both X and Y in start and end
        Assert.IsType<FinalPoint>(result.End);
        Assert.Equal(1.5, result.End.X);
        Assert.Equal(40, result.End.Y);
        Assert.Equal(4, result.Stroke);
        Assert.IsType<FinalColor>(result.StrokeColor);
    }
    
    [Fact]
    public void PassEvaluateExpressionSegLine() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst(
            "canvas (150, 150, Color(255, 255, 255, 1)); " +
            "segLine one = SegLine(Point(17, 6), Point(61, 82), 4, Color(175,6,135,1);") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("one", new Variable("one", GasType.SegLine));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalSegLine;
        
        Assert.NotNull(result);
        Assert.IsType<FinalSegLine>(result);
        Assert.IsType<FinalPoint>(result.Start);
        Assert.IsType<FinalPoint>(result.End);
        Assert.Equal(4, result.Stroke);
        Assert.IsType<FinalColor>(result.StrokeColor);
    }
    
    [Fact]
    public void PassEvaluateExpressionGroup() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var interpreter = new Interpreter();
        var ast = SharedTesting.GenerateAst(
            "canvas (250,250, Color(255,255,255,1));" +
            "group mouse = Group(Point(125, 80)," +
            "{" +
                "group mousEars = Group(Point(0, 0)," +
                "{" +
                "ellipse leftEar = Ellipse(Point(-30, 0), 30, 35, Color(255,255,255,1), Color(0,255,0,1), 10);," +
                "ellipse rightEar = Ellipse(Point(30, 0), 35, 30, Color(255,255,255,1), Color(255,0,0,1), 10);" +
            "});") as Compound;
        var scope = new Scope(null, null);
        scope.vTable.Bind("canvas", new Variable("canvas", GasType.Canvas));
        scope.vTable.Bind("mouse", new Variable("mouse", GasType.Group));
        var result = interpreter.EvaluateStatement(ast.Statement2, scope) as FinalGroup;
        
        Assert.NotNull(result);
        Assert.IsType<FinalGroup>(result);
        Assert.IsType<FinalPoint>(result.Point);
    }
}