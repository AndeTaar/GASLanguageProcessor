namespace GASLanguageProcessor.FinalTypes;

public class FinalText
{

    public string Text { get; set; }
    public FinalPoint Position { get; set; }
    public string Font { get; set; }
    public FinalNum FontSize { get; set; }

    public FinalColor TextColor { get; set; }

    public string ToCdataText()
    {
        var texts = this.Text.Split(' ');
        string str = "";
        foreach (var text in texts)
        {
            if(text == "")
            {
                str += " ";
                continue;
            }
            str += "<![CDATA[" + text + "]]> ";
        }

        return str;
    }

    public FinalText(string text, FinalPoint position, string font, float fontSize, FinalColor textColor)
    {
        Text = text;
        Position = position;
        Font = font;
        FontSize = new FinalNum(fontSize);
        TextColor = textColor;
    }
}