namespace Tests.AcceptanceTests;
using Xunit;
public class CircleByPolygonTest
{
    [Fact]
    public void CircleByPolygonTest1()
    {
        string directory = "AcceptanceTests/GasTestFiles/";
        var filePath =  Path.Combine(Directory.GetCurrentDirectory().Split("bin")[0], directory);

        var gas = File.ReadAllText(filePath + "CircleByPolygon.gas");
        var expected = File.ReadAllText(filePath + "CircleByPolygon.svg");

        var lines = SharedTesting.GetSvgLines(gas);
        var actual = string.Join("\n", lines);
        Assert.Equal(expected, actual);
    }
}
