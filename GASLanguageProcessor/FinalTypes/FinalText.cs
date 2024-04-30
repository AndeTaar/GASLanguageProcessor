namespace GASLanguageProcessor.FinalTypes;

public class FinalText: FinalObject
{

    public string Text { get; set; }
    public FinalPoint Position { get; set; }
    public string Font { get; set; }
    public float FontSize { get; set; }

    public FinalColour TextColour { get; set; }

    public FinalText(string text, FinalPoint position, string font, float fontSize, FinalColour textColour)
    {
        Text = text;
        Position = position;
        Font = font;
        FontSize = fontSize;
        TextColour = textColour;
    }
}