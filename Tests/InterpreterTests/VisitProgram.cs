using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class VisitProgram
{

    [Fact]
    public void VisitPassVisitProgramSemiLarge()
    {
        var ast = SharedTesting.GetInterpretedScope(
            "number Cos(number angle1) {\n" +
            "    number result = 1;\n" +
            "    number term = 1;\n" +
            "    number factorial = 1;\n" +
            "    for (number i = 1; i <= 10; i+=1) {\n" +
            "        term *= -angle1 * angle1 / ((2 * i) * (2 * i - 1));\n" +
            "        result += term;\n" +
            "    }\n" +
            "    return result;\n" +
            "}\n" +
            "\n" +
            "number Sin(number angle) {\n" +
            "    number result = angle;\n" +
            "    number term = angle;\n" +
            "    number factorial = 1;\n" +
            "    for (number i = 1; i <= 10; i+=1) {\n" +
            "        term *= -angle * angle / ((2 * i + 1) * (2 * i));\n" +
            "        result += term;\n" +
            "    }\n" +
            "    return result;\n" +
            "}\n" +
            "\n" +
            "\n" +
            "\nlist<point> points = List {\n" +
            "    Point(0, 0)\n};\n" +
            "\nnumber radius = 50;" +
            "\npoint center = Point(100, 100);\n" +
            "\nfor (number theta = 0; theta <= 2 * 3.14; theta = theta + 3.14 / 180)\n" +
            "{\n" +
            "    number x2 = 100 + radius * Cos(theta);\n" +
            "    number y = 100 + radius * Sin(theta);\n" +
            "    AddToList(Point(x2, y), points);\n" +
            "}\n" +
            "polygon poly = Polygon(points, 10, Color(0, 255, 255, 1), Color(255, 255, 255, 1));\n" +
            "canvas (250, 250, Color(255, 255, 255, 1));\n");

        var cosFunc = ast.fTable.LookUp("Cos");
        var sinFunc = ast.fTable.LookUp("Sin");

        Assert.NotNull(cosFunc);
        Assert.NotNull(sinFunc);
    }

    [Fact]
    public void VisitPassVisitProgramLargeProgram()
    {
        var ast = SharedTesting.GetInterpretedScope(
            "number Cos(number angle1) {\n" +
            "    number result = 1;\n" +
            "    number term = 1;\n" +
            "    number factorial = 1;\n" +
            "    for (number i = 1; i <= 10; i+=1) {\n" +
            "        term *= -angle1 * angle1 / ((2 * i) * (2 * i - 1));\n" +
            "        result += term;\n" +
            "    }\n" +
            "    return result;\n" +
            "}\n" +
            "\n" +
            "number Sin(number angle) {\n" +
            "    number result = angle;\n" +
            "    number term = angle;\n" +
            "    number factorial = 1;\n" +
            "    for (number i = 1; i <= 10; i+=1) {\n" +
            "        term *= -angle * angle / ((2 * i + 1) * (2 * i));\n" +
            "        result += term;\n" +
            "    }\n" +
            "    return result;\n" +
            "}\n" +
            "\n" +
            "\n" +
            "circle CircleCreator(point Center, number Width, number Stroke, color Fill, color StrokeColor)\n" +
            "{\n" +
            "    return Circle(Center, Width, Stroke, Fill, StrokeColor);\n" +
            "}\n" +
            "\nlist<circle> circles2 = List {\n" +
            "    CircleCreator(Point(50, 10), 40, 10, red, black),\n " +
            "   CircleCreator(Point(220, 120), 40, 10, red, black)\n};\n" +
            "\nlist<point> points = List {\n" +
            "    Point(0, 0)\n};\n" +
            "\nnumber radius = 50;" +
            "\npoint center = Point(100, 100);\n" +
            "\nfor (number theta = 0; theta <= 2 * 3.14; theta = theta + 3.14 / 180)\n" +
            "{\n" +
            "    number x2 = 100 + radius * Cos(theta);\n" +
            "    number y = 100 + radius * Sin(theta);\n" +
            "    AddToList(Point(x2, y), points);\n" +
            "}\n" +
            "polygon poly = Polygon(points, 10, Color(0, 255, 255, 1), Color(255, 255, 255, 1));\n" +
            "canvas (250, 250, Color(255, 255, 255, 1));\n" +
            "\n" +
            "\n" +
            "\n" +
            "if(false){\n" +
            "    number x = 0;\n" +
            "}else if(true){\n" +
            "    number x = 0;\n" +
            "  }else{\n" +
            "  number x = 0;\n" +
            "    number y = 0;\n" +
            "}\n" +
            "\n" +
            "number x = 0;\n" +
            "\n" +
            "square firkant = Square(Point(10,10), 230, 10, Color(255, 255, 255, 1), Color(255, 255, 255, 1));\n" +
            "\n" +
            "color white = Color(255, 255, 255, 1);\n" +
            "color green = Color(0, 255, 0, 1);\n" +
            "color red = Color(255, 0, 0, 1);\n" +
            "color pink = Color(255, 100, 100, 1);\n" +
            "color black = Color(0, 0, 0, 1);\n" +
            "\nlist<circle> circles = List {\n" +
            "    Circle(Point(0, 0), 1, 1, red, black)\n" +
            "};\n" +
            "\n" +
            "AddToList(CircleCreator(Point(50, 10), 33, 10, red, black), circles);\n" +
            "AddToList(CircleCreator(Point(100, 10), 36, 10, pink, white), circles);\n" +
            "AddToList(CircleCreator(Point(500, 10), 39, 10, green, black), circles);\n" +
            "\nfor(number i = 0; i < 10; i = i + 1)\n" +
            "{\n" +
            "    AddToList(CircleCreator(Point(50+i*10, 10), 40, 10, red, black), circles);\n" +
            "}\n" +
            "\n" +
            "for(number i = 0; x < 10; x = x + 1)\n" +
            "{\n" +
            "    square firkant2 = Square(Point(10, 10 + x * 50), 180, 10, green, pink);\n" +
            "    text txt2 = Text(\"Its even worse for mouses!\", Point(0,0 + x * 50), \"Arial\", 14, pink);\n" +
            "}\n" +
            "\n" +
            "\n" +
            "bool GetBool()\n" +
            "{\n" +
            "    return true;\n" +
            "}\n" +
            "\n" +
            "group mouse = Group(Point(145-20, 80),\n" +
            "{\n" +
            "    group mousEars = Group(Point(0, 0),\n" +
            "    {\n " +
            "       circle leftEar = CircleCreator(Point(-30, 0), 30, 10, white, pink);\n" +
            "        circle rightEar = CircleCreator(Point(30, 0), 30, 10, white, pink);\n" +
            "    });\n" +
            "\n    group mouseFace = Group(Point(0, 0),\n" +
            "    {\n" +
            "        circle face = CircleCreator(Point(0, 30), 50, 10, white, pink);\n" +
            "        circle eye = CircleCreator(Point(-20, 20), 10, 3, white, black);\n" +
            "        circle eye2 = CircleCreator(Point(20, 20), 10, 3, white, black);\n" +
            "        ellipse eyeball = Ellipse(Point(-20, 21), 5, 2, 2, black, black);\n" +
            "        ellipse eyeball2 = Ellipse(Point(20, 21), 5, 9, 2, black, black);\n" +
            "\n        segLine mouth = SegLine(Point(-20, 50), Point(20, 40), 3, black);\n" +
            "    });\n" +
            "});\n" +
            "\n" +
            "color reds = Color(255, 0, 0, 1);\n" +
            "point xy = Point(0, 0);\n" +
            "\n" +
            "number start = 180-40;\n" +
            "rectangle rectangle1 = Rectangle(Point(20, start+40), Point(230, start+70), 2, white, black);\n" +
            "string s1 = \"It's even worse\";\n" +
            "string s2 = \" for mouses!\";\n" +
            "text txt = Text(s1+s2, Point(29,200), \"Arial\", 16, black);\n" +
            "\n" +
            "line l1 = Line (0, 1, 2, Color(0, 0, 0, 1));\n" +
            "line l2 = Line (250, -1, 2, Color(0, 0, 0, 1));\n" +
            "\nstring j = \"a\" + \"b\" + \"c\";\n" +
            "bool k = true == false != true;\nbool l = 5<3 || 5>3 && 5<=5;\n" +
            ""
        );
        var cosFunc = ast.fTable.LookUp("Cos");
        var sinFunc = ast.fTable.LookUp("Sin");
        var circleCreatorFunc = ast.fTable.LookUp("CircleCreator");
        var getBoolFunc = ast.fTable.LookUp("GetBool");

        Assert.NotNull(cosFunc);
        Assert.NotNull(sinFunc);
        Assert.NotNull(circleCreatorFunc);
        Assert.NotNull(getBoolFunc);

        Assert.Equal(GasType.Number, cosFunc?.ReturnType);
        Assert.Equal(GasType.Number, sinFunc?.ReturnType);
        Assert.Equal(GasType.Circle, circleCreatorFunc?.ReturnType);
        Assert.Equal(GasType.Bool, getBoolFunc?.ReturnType);

        var groupMouse = ast.vTable.LookUp("mouse")?.ActualValue as FinalGroup;
        var groupMouseEars = ast?.vTable.LookUp("mousEars")?.ActualValue as FinalGroup;
        var groupMouseFace = ast?.vTable.LookUp("mouseFace")?.ActualValue as FinalGroup;
        var circleLeftEar = ast?.vTable.LookUp("leftEar")?.ActualValue as FinalCircle;
        var circleRightEar = ast?.vTable.LookUp("rightEar")?.ActualValue as FinalCircle;
        var circleFace = ast?.vTable.LookUp("face")?.ActualValue as FinalCircle;
        var circleEye = ast?.vTable.LookUp("eye")?.ActualValue as FinalCircle;
        var circleEye2 = ast?.vTable.LookUp("eye2")?.ActualValue as FinalCircle;
        var ellipseEyeball = ast?.vTable.LookUp("eyeball")?.ActualValue as FinalEllipse;
        var ellipseEyeball2 = ast?.vTable.LookUp("eyeball2")?.ActualValue as FinalEllipse;
        var segLineMouth = ast?.vTable.LookUp("mouth")?.ActualValue as FinalSegLine;

        Assert.NotNull(groupMouse);
        Assert.NotNull(groupMouseEars);
        Assert.NotNull(groupMouseFace);
        Assert.NotNull(circleLeftEar);
        Assert.NotNull(circleRightEar);
        Assert.NotNull(circleFace);
        Assert.NotNull(circleEye);
        Assert.NotNull(circleEye2);
        Assert.NotNull(ellipseEyeball);
        Assert.NotNull(ellipseEyeball2);
        Assert.NotNull(segLineMouth);
    }
}