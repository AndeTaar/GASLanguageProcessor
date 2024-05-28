namespace GASLanguageProcessor.FinalTypes;

public class FinalText
{

    public string Text { get; set; }
    public FinalPoint Position { get; set; }
    public string Font { get; set; }
    public FinalNum FontSize { get; set; }
    public FinalNum FontWeight { get; set; }

    public FinalColor TextColor { get; set; }

    public FinalText(string text, FinalPoint position, string font, float fontSize, float fontWeight, FinalColor textColor)
    {
        Text = text;
        Position = position;
        Font = font;
        FontSize = new FinalNum(fontSize);
        FontWeight = new FinalNum(fontWeight == 0 ? 400 : fontWeight ) ;
        TextColor = textColor;
    }
}