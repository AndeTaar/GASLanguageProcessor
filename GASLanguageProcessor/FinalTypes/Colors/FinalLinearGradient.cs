namespace GASLanguageProcessor.FinalTypes.Colors;

public class FinalLinearGradient: FinalColors
{
    public FinalLinearGradient(float alpha, float rotation, FinalList colors, FinalList stops)
    {
        Alpha = new FinalNum(alpha);
        Rotation = new FinalNum(rotation);
        Colors = colors;
        Stops = stops;
    }

    public FinalNum Rotation { get; set; }
    public FinalList Colors { get; set; }
    public FinalList Stops { get; set; }
    public string Id { get; set; }

    public override string ColorToString()
    {
        return $"url(#{Id})";
    }
}