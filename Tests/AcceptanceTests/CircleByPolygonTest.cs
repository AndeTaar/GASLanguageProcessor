namespace Tests.AcceptanceTests;
using Xunit;
public class CircleByPolygonTest
{
    [Fact]
    public void CircleByPolygonTest1()
    {
        string directory = "AcceptanceTests/CARLTestFiles/";
        var filePath =  Path.Combine(Directory.GetCurrentDirectory().Split("bin")[0], directory);

        var CARL = File.ReadAllText(filePath + "CircleByPolygon.CARL");
        var expected = File.ReadAllText(filePath + "CircleByPolygon.svg");

        var lines = SharedTesting.GetSvgLines(CARL);
        var actual = string.Join("\n", lines);
        Assert.Equal(expected, actual);
    }
}
