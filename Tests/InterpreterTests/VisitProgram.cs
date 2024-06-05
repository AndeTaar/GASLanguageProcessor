using GASLanguageProcessor.AST.Terms;
using GASLanguageProcessor.FinalTypes;

namespace Tests.OperationalSemantics.InterpreterTests.EvaluateExpressionTests;

public class VisitProgram
{
    [Fact]
    public void VisitPassVisitProgramSemiLarge()
    {
        var env = SharedTesting.RunInterpreter(
            "num Cos(num angle1) {\n" +
            "    num result = 1;\n" +
            "    num term = 1;\n" +
            "    num factorial = 1;\n" +
            "    for (num i = 1; i <= 10; i+=1) {\n" +
            "        term *= -angle1 * angle1 / ((2 * i) * (2 * i - 1));\n" +
            "        result += term;\n" +
            "    }\n" +
            "    return result;\n" +
            "}\n" +
            "\n" +
            "num Sin(num angle) {\n" +
            "    num result = angle;\n" +
            "    num term = angle;\n" +
            "    num factorial = 1;\n" +
            "    for (num i = 1; i <= 10; i+=1) {\n" +
            "        term *= -angle * angle / ((2 * i + 1) * (2 * i));\n" +
            "        result += term;\n" +
            "    }\n" +
            "    return result;\n" +
            "}\n" +
            "\n" +
            "\n" +
            "\nlist<point> points = List<point> {\n" +
            "    Point(0, 0)\n};\n" +
            "\nnum radius = 50;" +
            "\npoint center = Point(100, 100);\n" +
            "\nfor (num theta = 0; theta <= 2 * 3.14; theta = theta + 3.14 / 180)\n" +
            "{\n" +
            "    num x2 = 100 + radius * Cos(theta);\n" +
            "    num y = 100 + radius * Sin(theta);\n" +
            "    AddToList(Point(x2, y), points);\n" +
            "}\n" +
            "polygon poly = Polygon(points, 10, Color(0, 255, 255, 1), Color(255, 255, 255, 1));\n" +
            "canvas (250, 250, Color(255, 255, 255, 1));\n");

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var cosFunc = envF.LookUp("Cos");
        var sinFunc = envF.LookUp("Sin");

        Assert.NotNull(cosFunc);
        Assert.NotNull(sinFunc);
    }

    [Fact]
    public void VisitPassVisitProgramLargeProgram()
    {
        var env = SharedTesting.RunInterpreter(
            "num Cos(num angle1) {\n" +
            "    num result = 1;\n" +
            "    num term = 1;\n" +
            "    num factorial = 1;\n" +
            "    for (num i = 1; i <= 10; i+=1) {\n" +
            "        term *= -angle1 * angle1 / ((2 * i) * (2 * i - 1));\n" +
            "        result += term;\n" +
            "    }\n" +
            "    return result;\n" +
            "}\n" +
            "\n" +
            "num Sin(num angle) {\n" +
            "    num result = angle;\n" +
            "    num term = angle;\n" +
            "    num factorial = 1;\n" +
            "    for (num i = 1; i <= 10; i+=1) {\n" +
            "        term *= -angle * angle / ((2 * i + 1) * (2 * i));\n" +
            "        result += term;\n" +
            "    }\n" +
            "    return result;\n" +
            "}\n" +
            "\n" +
            "color white = Color(255, 255, 255, 1);\n" +
            "color green = Color(0, 255, 0, 1);\n" +
            "color red = Color(255, 0, 0, 1);\n" +
            "color pink = Color(255, 100, 100, 1);\n" +
            "color black = Color(0, 0, 0, 1);\n" +
            "\n" +
            "circle CircleCreator(point Center, num Width, num Stroke, color Fill, color StrokeColor)\n" +
            "{\n" +
            "    return Circle(Center, Width, Stroke, Fill, StrokeColor);\n" +
            "}\n" +
            "\nlist<circle> circles2 = List<circle> {\n" +
            "    CircleCreator(Point(50, 10), 40, 10, red, black),\n " +
            "   CircleCreator(Point(220, 120), 40, 10, red, black)\n};\n" +
            "\nlist<point> points = List<point> {\n" +
            "    Point(0, 0)\n};\n" +
            "\nnum radius = 50;" +
            "\npoint center = Point(100, 100);\n" +
            "\nfor (num theta = 0; theta <= 2 * 3.14; theta = theta + 3.14 / 180)\n" +
            "{\n" +
            "    num x2 = 100 + radius * Cos(theta);\n" +
            "    num y = 100 + radius * Sin(theta);\n" +
            "    AddToList(Point(x2, y), points);\n" +
            "}\n" +
            "polygon poly = Polygon(points, 10, Color(0, 255, 255, 1), Color(255, 255, 255, 1));\n" +
            "canvas (250, 250, Color(255, 255, 255, 1));\n" +
            "\n" +
            "\n" +
            "\n" +
            "if(false){\n" +
            "    num x = 0;\n" +
            "}else if(true){\n" +
            "    num x = 0;\n" +
            "  }else{\n" +
            "  num x = 0;\n" +
            "    num y = 0;\n" +
            "}\n" +
            "\n" +
            "num x = 0;\n" +
            "\n" +
            "square firkant = Square(Point(10,10), 230, 10, Color(255, 255, 255, 1), Color(255, 255, 255, 1), 10);\n" +
            "\n" +
            "\nlist<circle> circles = List<circle> {\n" +
            "    Circle(Point(0, 0), 1, 1, red, black)\n" +
            "};\n" +
            "\n" +
            "AddToList(CircleCreator(Point(50, 10), 33, 10, red, black), circles);\n" +
            "AddToList(CircleCreator(Point(100, 10), 36, 10, pink, white), circles);\n" +
            "AddToList(CircleCreator(Point(500, 10), 39, 10, green, black), circles);\n" +
            "\nfor(num i = 0; i < 10; i = i + 1)\n" +
            "{\n" +
            "    AddToList(CircleCreator(Point(50+i*10, 10), 40, 10, red, black), circles);\n" +
            "}\n" +
            "\n" +
            "for(num i = 0; x < 10; x = x + 1)\n" +
            "{\n" +
            "    square firkant2 = Square(Point(10, 10 + x * 50), 180, 10, green, pink, 10);\n" +
            "    text txt2 = Text(\"Its even worse for mouses!\", Point(0,0 + x * 50), \"Arial\", 14, 400, pink);\n" +
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
            "       circle rightEar = CircleCreator(Point(30, 0), 30, 10, white, pink);\n" +
            "    });\n" +
            "\n    group mouseFace = Group(Point(0, 0),\n" +
            "    {\n" +
            "        circle face = CircleCreator(Point(0, 30), 50, 10, white, pink);\n" +
            "        circle eye = CircleCreator(Point(-20, 20), 10, 3, white, black);\n" +
            "        circle eye2 = CircleCreator(Point(20, 20), 10, 3, white, black);\n" +
            "        ellipse eyeball = Ellipse(Point(-20, 21), 5, 2, 2, black, black);\n" +
            "        ellipse eyeball2 = Ellipse(Point(20, 21), 5, 9, 2, black, black);\n" +
            "\n      segLine mouth = SegLine(Point(-20, 50), Point(20, 40), 3, black);\n" +
            "    });\n" +
            "});\n" +
            "\n" +
            "color reds = Color(255, 0, 0, 1);\n" +
            "point xy = Point(0, 0);\n" +
            "\n" +
            "num start = 180-40;\n" +
            "rectangle rectangle1 = Rectangle(Point(20, start+40), Point(230, start+70), 2, white, black, 10);\n" +
            "string s1 = \"It's even worse\";\n" +
            "string s2 = \" for mouses!\";\n" +
            "text txt = Text(s1+s2, Point(29,200), \"Arial\", 16, 400, black);\n" +
            "\n" +
            "line l1 = Line (0, 1, 2, Color(0, 0, 0, 1));\n" +
            "line l2 = Line (250, -1, 2, Color(0, 0, 0, 1));\n" +
            "\nstring j = \"a\" + \"b\" + \"c\";\n" +
            "bool k = true == false != true;\nbool l = 5<3 || 5>3 && 5<=5;\n" +
            ""
        );

        var envV = env.Item1;
        var sto = env.Item2;
        var envT = env.Item3;
        var envF = env.Item4;
        var errors = env.Item5;
        Assert.Empty(errors);

        var cosFunc = envF.LookUp("Cos");
        var sinFunc = envF.LookUp("Sin");
        var circleCreatorFunc = envF.LookUp("CircleCreator");
        var getBoolFunc = envF.LookUp("GetBool");

        Assert.NotNull(cosFunc);
        Assert.NotNull(sinFunc);
        Assert.NotNull(circleCreatorFunc);
        Assert.NotNull(getBoolFunc);

        var cosFuncType = envT.FLookUp("Cos");
        var sinFuncType = envT.FLookUp("Sin");
        var circleCreatorFuncType = envT.FLookUp("CircleCreator");
        var getBoolFuncType = envT.FLookUp("GetBool");

        Assert.NotNull(cosFuncType);
        Assert.NotNull(sinFuncType);
        Assert.NotNull(circleCreatorFuncType);
        Assert.NotNull(getBoolFuncType);

        Assert.Equal(GasType.Num, cosFuncType.Value.Item2);
        Assert.Equal(GasType.Num, sinFuncType.Value.Item2);
        Assert.Equal(GasType.Circle, circleCreatorFuncType.Value.Item2);
        Assert.Equal(GasType.Bool, getBoolFuncType.Value.Item2);

        var grMousLoc = envV.LookUp("mouse");
        Assert.NotNull(grMousLoc);

        var groupMouse = sto.LookUp(grMousLoc.Value) as FinalGroup;
        Assert.NotNull(groupMouse);

        envV = groupMouse.EnvV;
        var grMousEarsLoc = envV.LookUp("mousEars");
        var grMousFaceLoc = envV.LookUp("mouseFace");
        Assert.NotNull(grMousEarsLoc);
        Assert.NotNull(grMousFaceLoc);

        var groupMouseEars = sto.LookUp(grMousEarsLoc.Value) as FinalGroup;
        var groupMouseFace = sto.LookUp(grMousFaceLoc.Value) as FinalGroup;
        Assert.NotNull(groupMouseEars);
        Assert.NotNull(groupMouseFace);

        var mouseEarsEnvV = groupMouseEars.EnvV;
        var circleLeftEar = sto.LookUp(mouseEarsEnvV.LookUp("leftEar").Value) as FinalCircle;
        var circleRightEar = sto.LookUp(mouseEarsEnvV.LookUp("rightEar").Value) as FinalCircle;
        Assert.NotNull(circleLeftEar);
        Assert.NotNull(circleRightEar);

        var mouseFaceEnvV = groupMouseFace.EnvV;
        var circleFace = sto.LookUp(mouseFaceEnvV.LookUp("face").Value) as FinalCircle;
        var circleEye = sto.LookUp(mouseFaceEnvV.LookUp("eye").Value) as FinalCircle;
        var circleEye2 = sto.LookUp(mouseFaceEnvV.LookUp("eye2").Value) as FinalCircle;
        var ellipseEyeball = sto.LookUp(mouseFaceEnvV.LookUp("eyeball").Value) as FinalEllipse;
        var ellipseEyeball2 = sto.LookUp(mouseFaceEnvV.LookUp("eyeball2").Value) as FinalEllipse;
        var segLineMouth = sto.LookUp(mouseFaceEnvV.LookUp("mouth").Value) as FinalSegLine;

        Assert.NotNull(circleFace);
        Assert.NotNull(circleEye);
        Assert.NotNull(circleEye2);
        Assert.NotNull(ellipseEyeball);
        Assert.NotNull(ellipseEyeball2);
        Assert.NotNull(segLineMouth);
    }
}