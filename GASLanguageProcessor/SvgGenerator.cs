using Antlr4.Runtime.Misc;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class SvgGenerator
{
    // Add name or something
    public string FilePath = Path.Combine(Directory.GetCurrentDirectory().Split("Output\\")[0], "svgtest.svg");
    public ArrayList<string> SvgLines = new();

    public void GenerateSvg(VariableTable vTable) //still only takes variables from global scope and it does not look as expected
    {
        foreach (Variable variable in vTable.Variables.Values)
        {
            switch (variable.ActualValue)
            {
                case FinalCanvas canvas: // Background color either as CSS style or as a rectangle that fills the canvas area. CSS for now.
                    SvgLines.Add($"<svg width=\"{canvas.Width}\" height=\"{canvas.Height}\" style=\"background-color: {canvas.BackgroundColour.ColourToString()}\">");
                    break;
                case FinalCircle circle: // "stroke" in SVG is our StrokeColour
                    SvgLines.Add($"<circle cx=\"{circle.Center.X}\" cy=\"{circle.Center.Y}\" r=\"{circle.Radius}\" fill=\"{circle.FillColour.ColourToString()}\" fill-opacity=\"{circle.FillColour.Alpha}\" stroke=\"{circle.StrokeColour.ColourToString()}\" stroke-width=\"{circle.Stroke}\" />");
                    break;
                case FinalLine line:
                    SvgLines.Add($"<line x1=\"{line.Start.X}\" y1=\"{line.Start.Y}\" x2=\"{line.End.X}\" y2=\"{line.End.Y}\" stroke=\"{line.StrokeColour.ColourToString()}\" stroke-width=\"{line.Stroke}\" />");
                    break;
                case FinalRectangle rectangle:
                    SvgLines.Add($"<rect x=\"{rectangle.TopLeft.X}\" y=\"{rectangle.TopLeft.Y}\" width=\"{rectangle.Width}\" height=\"{rectangle.Height}\" fill=\"{rectangle.FillColour.ColourToString()}\" fill-opacity=\"{rectangle.FillColour.Alpha}\" stroke=\"{rectangle.StrokeColour.ColourToString()}\" stroke-width=\"{rectangle.Stroke}\" />");
                    break; //notice: our "stroke" is the stroke width in SVG and our "StrokeColour" is the stroke in SVG
                case FinalText text:
                    SvgLines.Add($"<text x=\"{text.Position.X}\" y=\"{text.Position.Y}\" fill=\"{text.TextColour.ColourToString()}\" font-family={text.Font} font-size=\"{text.FontSize}\">{text.Text}</text>");
                    break;
                case FinalSquare square:
                    SvgLines.Add($"<rect x=\"{square.TopLeft.X}\" y=\"{square.TopLeft.Y}\" width=\"{square.Length}\" height=\"{square.Length}\" fill=\"{square.FillColour.ColourToString()}\" fill-opacity=\"{square.FillColour.Alpha}\" stroke=\"{square.StrokeColour.ColourToString()}\" stroke-width=\"{square.Stroke}\" />");
                    break;
            }
        }

        SvgLines.Add("</svg>");
        File.WriteAllLines(FilePath, SvgLines);
    }
}
