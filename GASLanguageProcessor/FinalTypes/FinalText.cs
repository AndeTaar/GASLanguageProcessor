using GASLanguageProcessor.FinalTypes.Colors;

namespace GASLanguageProcessor.FinalTypes;

public class FinalText : FinalType
{
    public FinalText(string text, FinalPoint position, string font, float fontSize, float fontWeight,
        FinalColors textColor)
    {
        Text = text;
        Position = position;
        Font = font;
        FontSize = new FinalNum(fontSize);
        FontWeight = new FinalNum(fontWeight == 0 ? 400 : fontWeight);
        TextColor = textColor;
    }

    public string Text { get; set; }
    public FinalPoint Position { get; set; }
    public string Font { get; set; }
    public FinalNum FontSize { get; set; }
    public FinalNum FontWeight { get; set; }

    public FinalColors TextColor { get; set; }
}
