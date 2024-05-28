namespace GASLanguageProcessor.FinalTypes;

public class FinalPolygon
{
    public FinalList Points { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColors Colors { get; set; }
    public FinalColors StrokeColors { get; set; }

    public FinalPolygon(FinalList points, float stroke, FinalColors colors, FinalColors strokeColors)
    {
        Points = points;
        Colors = colors;
        Stroke = new FinalNum(stroke);
        StrokeColors = strokeColors;
    }

}
