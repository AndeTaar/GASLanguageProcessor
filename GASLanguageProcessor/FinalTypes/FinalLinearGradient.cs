namespace GASLanguageProcessor.FinalTypes;

public class FinalLinearGradient : FinalColors
{
    public FinalNum Rotation { get; protected set; }
    public FinalList ColorList { get; protected set; }
    public FinalList PercentagesList { get; protected set; }
    public string Id { get; set; }

    public FinalLinearGradient(float alpha, float rotation, FinalList colorList, FinalList percentagesList)
    {
        Rotation = new FinalNum(rotation);
        Alpha = new FinalNum(alpha);
        ColorList = colorList;
        PercentagesList = percentagesList;
    }

    public override string ColorToString()
    {
        return $"url(#{Id})";
    }
}
