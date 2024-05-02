using Antlr4.Runtime.Misc;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class SvgGenerator
{
    public ArrayList<string> SvgLines = new();

    public ArrayList<string> GenerateSvg(VariableTable vTable) //still only takes variables from global scope and it does not look as expected
    {
        foreach (Variable variable in vTable.Variables.Values)
        {
            SvgLines.Add(GenerateLine(variable.ActualValue, variable));
        }

        return SvgLines;
    }

    public string GenerateLine(Object obj, Variable? variable = null)
    {
         switch (obj)
            {
                case FinalCanvas canvas: // Background color either as CSS style or as a rectangle that fills the canvas area. CSS for now.
                    return
                        $"<svg width=\"{canvas.Width}\" height=\"{canvas.Height}\" style=\"background-color: {canvas.BackgroundColour.ColourToString()}\" xmlns=\"http://www.w3.org/2000/svg\">";
                case FinalCircle circle: // "stroke" in SVG is our StrokeColour
                    return $"<circle id=\"{variable?.Identifier}\" cx=\"{circle.Center.X}\" cy=\"{circle.Center.Y}\" r=\"{circle.Radius}\" fill=\"{circle.FillColour.ColourToString()}\" fill-opacity=\"{circle.FillColour.Alpha}\" stroke=\"{circle.StrokeColour.ColourToString()}\" stroke-width=\"{circle.Stroke}\" />";
                case FinalLine line:
                    return $"<line id=\"{variable?.Identifier}\" x1=\"{line.Start.X}\" y1=\"{line.Start.Y}\" x2=\"{line.End.X}\" y2=\"{line.End.Y}\" stroke=\"{line.StrokeColour.ColourToString()}\" stroke-width=\"{line.Stroke}\" />";
                case FinalRectangle rectangle:
                     return $"<rect id=\"{variable?.Identifier}\" x=\"{rectangle.TopLeft.X}\" y=\"{rectangle.TopLeft.Y}\" width=\"{rectangle.Width}\" height=\"{rectangle.Height}\" fill=\"{rectangle.FillColour.ColourToString()}\" fill-opacity=\"{rectangle.FillColour.Alpha}\" stroke=\"{rectangle.StrokeColour.ColourToString()}\" stroke-width=\"{rectangle.Stroke}\" />";
                    //notice: our "stroke" is the stroke width in SVG and our "StrokeColour" is the stroke in SVG
                case FinalText text:
                     return $"<text id=\"{variable?.Identifier}\" x=\"{text.Position.X}\" y=\"{text.Position.Y}\" fill=\"{text.TextColour.ColourToString()}\" font-family=\"{text.Font}\" font-size=\"{text.FontSize}\">{text.Text}</text>";
                case FinalSquare square:
                    return $"<rect id=\"{variable?.Identifier}\" x=\"{square.TopLeft.X}\" y=\"{square.TopLeft.Y}\" width=\"{square.Length}\" height=\"{square.Length}\" fill=\"{square.FillColour.ColourToString()}\" fill-opacity=\"{square.FillColour.Alpha}\" stroke=\"{square.StrokeColour.ColourToString()}\" stroke-width=\"{square.Stroke}\" />";
                case FinalGroup group:
                    var resultGroup = "";
                    for (int i = 0; i < group.Values.Count; i++)
                    {
                        resultGroup += GenerateLine(group.Values[i], variable);
                    }
                    return resultGroup;
                case FinalList list:
                    var resultList = "";
                    for (int i = 0; i < list.Values.Count; i++)
                    {
                        resultList += GenerateLine(list.Values[i], variable);
                    }

                    return resultList;
            }

            return "";
    }
}
