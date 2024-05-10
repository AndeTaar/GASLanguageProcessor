namespace Tests.GeneratorTests;

public class GenerateLineTests
{
    [Fact]
    public void GenerateLineCanvasPass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));"
        );
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("</svg>", svgLines[1]);
    }
    
    [Fact]
    public void GenerateLineCirclePass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "circle c = Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1));"
        );
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("<circle id=\"c\" cx=\"10\" cy=\"20\" r=\"30\"" +
                     " fill=\"rgba(255, 0, 0, 1)\" fill-opacity=\"1\"" +
                     " stroke=\"rgba(0, 255, 0, 1)\" stroke-width=\"10\" />", svgLines[1]);
        Assert.Equal("</svg>", svgLines[2]);
    }
    
    [Fact]
    public void GenerateLineSegLinePass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "segLine sl = SegLine(Point(10, 20), Point(30, 40), 10, Color(255, 0, 0, 1));"
        );
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("<line id=\"sl\" x1=\"10\" y1=\"20\" x2=\"30\" y2=\"40\"" +
                     " stroke=\"rgba(255, 0, 0, 1)\" stroke-width=\"10\" />", svgLines[1]);
        Assert.Equal("</svg>", svgLines[2]);
    }
    
    [Fact]
    public void GenerateLineLinePass()
    {
        float lineGradient = 3, lineIntercept = 100;
        float canvasHeight = 250 * 2, canvasWidth = 10 * 50;
        var svgLines = SharedTesting.GetSvgLines(
            $"canvas ({canvasWidth}, {canvasHeight}, Color(255, 255, 255, 1));" +
            $"line l = Line({lineIntercept}, {lineGradient}, 10, Color(255, 0, 0, 1));"
        );
        
        float expectedLineEndX = (canvasHeight - lineIntercept) / lineGradient + 1;
        float expectedLineEndY = lineGradient * expectedLineEndX + lineIntercept;
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal($"<line id=\"l\" x1=\"0\" y1=\"100\" x2=\"{expectedLineEndX}\" y2=\"{expectedLineEndY}\"" +
                     " stroke=\"rgba(255, 0, 0, 1)\" stroke-width=\"10\" />", svgLines[1]);
        Assert.Equal("</svg>", svgLines[2]);
    }

    [Fact]
    public void GenerateLineRectanglePass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "rectangle r = Rectangle(Point(10, 20), Point(100, 250), 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1));"
        );

        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("<rect id=\"r\" x=\"10\" y=\"20\" width=\"90\" height=\"230\"" + " fill=\"rgba(255, 0, 0, 1)\"" +
                     " fill-opacity=\"1\" stroke=\"rgba(0, 255, 0, 1)\" stroke-width=\"10\" />", svgLines[1]);
        Assert.Equal("</svg>", svgLines[2]);
    }
    
    [Fact]
    public void GenerateLineEllipsePass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "ellipse e = Ellipse(Point(10, 20), 30, 40, Color(255, 0, 0, 1), Color(0, 255, 0, 1), 10);"
        );
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("<ellipse id=\"e\" cx=\"10\" cy=\"20\" rx=\"30\" ry=\"40\"" +
                     " fill=\"rgba(255, 0, 0, 1)\" stroke=\"rgba(0, 255, 0, 1)\" stroke-width=\"10\" />", svgLines[1]);
        Assert.Equal("</svg>", svgLines[2]);
    }
    
    [Fact]
    public void GenerateLineTextPass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "text t = Text(\"Hello, World!\", Point(10, 20), \"Arial\", 20, Color(255, 0, 0, 1));"
        );
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("<text id=\"t\" x=\"10\" y=\"20\" fill=\"rgba(255, 0, 0, 1)\"" +
                     " font-family=\"Arial\" font-size=\"20\">Hello, World!</text>", svgLines[1]);
        Assert.Equal("</svg>", svgLines[2]);
    }
    
    [Fact]
    public void GenerateLineSquarePass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "square s = Square(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1));"
        );
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("<rect id=\"s\" x=\"10\" y=\"20\" width=\"30\" height=\"30\"" +
                     " fill=\"rgba(255, 0, 0, 1)\" fill-opacity=\"1\"" +
                     " stroke=\"rgba(0, 255, 0, 1)\" stroke-width=\"10\" />", svgLines[1]);
        Assert.Equal("</svg>", svgLines[2]);
    }
    
    [Fact]
    public void GenerateLineGroupPass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "group g = Group(Point(0,0)," +
            "{" +
            "   circle groupCircle = Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1));" +
            "   square groupSquare = Square(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1));" +
            "});"
        );
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("<g id=\"g\" transform=\"translate(0, 0)\">", svgLines[1]);
        Assert.Equal("<circle id=\"groupCircle\" cx=\"10\" cy=\"20\" r=\"30\"" +
                     " fill=\"rgba(255, 0, 0, 1)\" fill-opacity=\"1\"" +
                     " stroke=\"rgba(0, 255, 0, 1)\" stroke-width=\"10\" />", svgLines[2]);
        Assert.Equal("<rect id=\"groupSquare\" x=\"10\" y=\"20\" width=\"30\" height=\"30\"" +
                     " fill=\"rgba(255, 0, 0, 1)\" fill-opacity=\"1\"" +
                     " stroke=\"rgba(0, 255, 0, 1)\" stroke-width=\"10\" />", svgLines[3]);
        Assert.Equal("</g>", svgLines[4]);
        Assert.Equal("</svg>", svgLines[5]);
    }
    
    [Fact]
    public void GenerateLineListPass()
    {
        var svgLines = SharedTesting.GetSvgLines(
            "canvas (250 * 2, 10 * 50, Color(255, 255, 255, 1));" +
            "list<circle> l = List" +
            "{" +
            "   Circle(Point(10, 20), 30, 10, Color(255, 0, 0, 1), Color(0, 255, 0, 1))," +
            "   Circle(Point(100, 200), 30, 10, Color(0, 255, 0, 1), Color(0, 0, 255, 1))" +
            "};"
        );
        
        Assert.NotEmpty(svgLines);
        Assert.All(svgLines, line => Assert.IsType<string>(line));
        Assert.Equal("<svg width=\"500\" height=\"500\"" +
                     " style=\"background-color: rgba(255, 255, 255, 1)\"" +
                     " xmlns=\"http://www.w3.org/2000/svg\">", svgLines[0]);
        Assert.Equal("<circle id=\"l\" cx=\"10\" cy=\"20\" r=\"30\"" +
                     " fill=\"rgba(255, 0, 0, 1)\" fill-opacity=\"1\"" +
                     " stroke=\"rgba(0, 255, 0, 1)\" stroke-width=\"10\" />", svgLines[1]);
        Assert.Equal("<circle id=\"l\" cx=\"100\" cy=\"200\" r=\"30\"" +
                     " fill=\"rgba(0, 255, 0, 1)\" fill-opacity=\"1\"" +
                     " stroke=\"rgba(0, 0, 255, 1)\" stroke-width=\"10\" />", svgLines[2]);
        Assert.Equal("</svg>", svgLines[3]);
    }
}