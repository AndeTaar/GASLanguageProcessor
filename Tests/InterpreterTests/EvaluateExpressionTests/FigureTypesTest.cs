using Antlr4.Runtime;
using GASLanguageProcessor;
using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.AST.Expressions.Terms;
using GASLanguageProcessor.AST.Statements;
using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class FigureTypesTest
{
    [Fact]
    public void PassEvaluateExpressionColor() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "colors notYellow = Colors(184,62,17,0.65);");

        var result = scope.vTable.LookUp("notYellow")?.ActualValue as FinalColors;
        var expected = new FinalColors(184, 62, 17, 0.65f);

        Assert.NotNull(result);
        Assert.IsType<FinalColors>(result);
        Assert.Equal(expected.Red.Value, result.Red.Value);
        Assert.Equal(expected.Green.Value, result.Green.Value);
        Assert.Equal(expected.Blue.Value, result.Blue.Value);
        Assert.Equal(expected.Alpha.Value, result.Alpha.Value);
    }

    [Fact]
    public void PassEvaluateExpressionPoint() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "point one = Point(30,50);");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalPoint;
        var expected = new FinalPoint(30, 50);

        Assert.NotNull(result);
        Assert.IsType<FinalPoint>(result);
        Assert.Equal(expected.X.Value, result.X.Value);
        Assert.Equal(expected.Y.Value, result.Y.Value);
    }

    [Fact]
    public void PassEvaluateExpressionSqaure() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "square one = Square(Point(10,2), 40, 4, Colors(255,0,255,1), Colors(255,255,0,1), 10);");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalSquare;
        var expected = new FinalSquare(new FinalPoint(10, 2), 40, 4,
                    new FinalColors(255, 0, 255, 1),
                    new FinalColors(255, 255, 0, 1),
                    10);

        Assert.NotNull(result);
        Assert.IsType<FinalSquare>(result);
        Assert.IsType<FinalPoint>(result.TopLeft);
        Assert.Equal(expected.Length.Value, result.Length.Value);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        Assert.IsType<FinalColors>(result.FillColors);
        Assert.IsType<FinalColors>(result.StrokeColors);
    }

    [Fact]
    public void PassEvaluateExpressionEllipse() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "ellipse one = Ellipse(Point(10,2), 5, 10, 4, Colors(255,0,255,1), Colors(255,255,0,1));");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalEllipse;
        var expected = new FinalEllipse(new FinalPoint(10, 2), 5, 10, 4,
                    new FinalColors(255, 0, 255, 1),
                    new FinalColors(255, 255, 0, 1));

        Assert.NotNull(result);
        Assert.IsType<FinalEllipse>(result);
        Assert.IsType<FinalPoint>(result.Center);
        Assert.Equal(expected.RadiusX.Value, result.RadiusX.Value);
        Assert.Equal(expected.RadiusY.Value, result.RadiusY.Value);
        Assert.IsType<FinalColors>(result.Colors);
        Assert.IsType<FinalColors>(result.StrokeColor);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
    }

    [Fact]
    public void PassEvaluateExpressionText() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "text one = Text(\"Hello World\", Point(67,37), \"Arial\", 24, 400, Colors(0,255,255,1));");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalText;
        var expected = new FinalText("Hello World", new FinalPoint(67, 37), "Arial", 24, 400, new FinalColors(0, 255, 255, 1));

        Assert.NotNull(result);
        Assert.IsType<FinalText>(result);
        Assert.Equal(expected.Text, result.Text);
        Assert.IsType<FinalPoint>(result.Position);
        Assert.Equal(expected.Font, result.Font);
        Assert.Equal(expected.FontSize.Value, result.FontSize.Value);
        Assert.IsType<FinalColors>(result.TextColors);
    }

    [Fact]
    public void PassEvaluateExpressionCircle() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "circle one = Circle(Point(10,2), 5, 7, Colors(255,255,0,1), Colors(175,6,135,1));");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalCircle;
        var expected = new FinalCircle(new FinalPoint(10, 2), 5, 7,
                    new FinalColors(255, 255, 0, 1),
                    new FinalColors(175, 6, 135, 1));

        Assert.NotNull(result);
        Assert.IsType<FinalCircle>(result);
        Assert.IsType<FinalPoint>(result.Center);
        Assert.Equal(expected.Radius.Value, result.Radius.Value);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        Assert.IsType<FinalColors>(result.FillColors);
        Assert.IsType<FinalColors>(result.StrokeColors);
    }

    [Fact]
    public void PassEvaluateExpressionRectangle() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "rectangle one = Rectangle(Point(5,2), Point(10,4), 9, Colors(255,255,0,1), Colors(175,6,135,1),1);");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalRectangle;
        var expected = new FinalRectangle(new FinalPoint(5, 2), new FinalPoint(10, 4), 9,
                    new FinalColors(255, 255, 0, 1),
                    new FinalColors(175, 6, 135, 1),0);

        Assert.NotNull(result);
        Assert.IsType<FinalRectangle>(result);
        Assert.IsType<FinalPoint>(result.TopLeft);
        Assert.IsType<FinalPoint>(result.BottomRight);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
        Assert.IsType<FinalColors>(result.FillColors);
        Assert.IsType<FinalColors>(result.StrokeColors);
    }

    [Fact]
    public void PassEvaluateExpressionLine() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "line one = Line(42, 7, 4, Colors(64,29,11,1));");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalLine;

        Assert.NotNull(result);
        Assert.IsType<FinalLine>(result);
        Assert.IsType<FinalPoint>(result.Start);
        Assert.IsType<FinalPoint>(result.End);
        Assert.IsType<FinalColors>(result.StrokeColors);
        Assert.Equal(4, result.Stroke.Value);
        // Consider adding asserts for the actual values of the points
    }

    [Fact]
    public void PassEvaluateExpressionSegLine() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "segLine one = SegLine(Point(17, 6), Point(61, 82), 4, Colors(175,6,135,1));");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalSegLine;
        var expected = new FinalSegLine(new FinalPoint(17, 6), new FinalPoint(61, 82), 4, new FinalColors(175, 6, 135, 1));

        Assert.NotNull(result);
        Assert.IsType<FinalSegLine>(result);
        Assert.IsType<FinalPoint>(result.Start);
        Assert.IsType<FinalPoint>(result.End);
        Assert.IsType<FinalColors>(result.StrokeColors);
        Assert.Equal(expected.Stroke.Value, result.Stroke.Value);
    }

    [Fact]
    public void PassEvaluateExpressionGroup() //canvas is needed since GenerateAst uses parser.program() and program needs canvas
    {
        var scope = SharedTesting.GetInterpretedScope("canvas (150, 150, Colors(255, 255, 255, 1));" +
                                        "group one = Group(Point(17, 6), {" +
                                        "   circle c = Circle(Point(10,2), 5, 7, Colors(255,255,0,1), Colors(175,6,135,1));" +
                                        "});");

        var result = scope.vTable.LookUp("one")?.ActualValue as FinalGroup;

        Assert.NotNull(result);
        Assert.IsType<FinalGroup>(result);
        Assert.IsType<FinalPoint>(result.Point);
    }
}