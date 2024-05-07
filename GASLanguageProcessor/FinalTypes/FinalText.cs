namespace GASLanguageProcessor.FinalTypes;

public class FinalText
{

    public string Text { get; set; }
    public FinalPoint Position { get; set; }
    public string Font { get; set; }
    public float FontSize { get; set; }

    public FinalColor TextColor { get; set; }

    public FinalText(string text, FinalPoint position, string font, float fontSize, FinalColor textColor)
    {
        Text = text;
        Position = position;
        Font = font;
        FontSize = fontSize;
        TextColor = textColor;
    }
}