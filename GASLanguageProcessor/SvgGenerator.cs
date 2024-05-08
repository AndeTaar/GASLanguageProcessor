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
            GenerateLine(variable.ActualValue, variable);
        }

        return SvgLines;
    }

   public void GenerateLine(Object obj, Variable? variable = null)
{
    switch (obj)
    {
        case FinalCanvas canvas:
            SvgLines.Add($"<svg width=\"{canvas.Width}\" height=\"{canvas.Height}\" style=\"background-color: {canvas.BackgroundColor.ColorToString()}\" xmlns=\"http://www.w3.org/2000/svg\">");
            break;
        case FinalCircle circle:
            SvgLines.Add($"<circle id=\"{variable?.Identifier}\" cx=\"{circle.Center.X}\" cy=\"{circle.Center.Y}\" r=\"{circle.Radius}\" fill=\"{circle.FillColor.ColorToString()}\" fill-opacity=\"{circle.FillColor.Alpha}\" stroke=\"{circle.StrokeColor.ColorToString()}\" stroke-width=\"{circle.Stroke}\" />");
            break;
        case FinalLine line:
            SvgLines.Add($"<line id=\"{variable?.Identifier}\" x1=\"{line.Start.X}\" y1=\"{line.Start.Y}\" x2=\"{line.End.X}\" y2=\"{line.End.Y}\" stroke=\"{line.StrokeColor.ColorToString()}\" stroke-width=\"{line.Stroke}\" />");
            break;
        case FinalRectangle rectangle:
            SvgLines.Add($"<rect id=\"{variable?.Identifier}\" x=\"{rectangle.TopLeft.X}\" y=\"{rectangle.TopLeft.Y}\" width=\"{rectangle.Width}\" height=\"{rectangle.Height}\" fill=\"{rectangle.FillColor.ColorToString()}\" fill-opacity=\"{rectangle.FillColor.Alpha}\" stroke=\"{rectangle.StrokeColor.ColorToString()}\" stroke-width=\"{rectangle.Stroke}\" />");
            break;
        case FinalSegLine segLine:
            SvgLines.Add($"<line id=\"{variable?.Identifier}\" x1=\"{segLine.Start.X}\" y1=\"{segLine.Start.Y}\" x2=\"{segLine.End.X}\" y2=\"{segLine.End.Y}\" stroke=\"{segLine.StrokeColor.ColorToString()}\" stroke-width=\"{segLine.Stroke}\" />");
            break;
        case FinalEllipse ellipse:
            SvgLines.Add($"<ellipse id=\"{variable?.Identifier}\" cx=\"{ellipse.Center.X}\" cy=\"{ellipse.Center.Y}\" rx=\"{ellipse.RadiusX}\" ry=\"{ellipse.RadiusY}\" fill=\"{ellipse.Color.ColorToString()}\" stroke=\"{ellipse.BorderColor.ColorToString()}\" stroke-width=\"{ellipse.BorderWidth}\" />");
            break;
        case FinalText text:
            SvgLines.Add($"<text id=\"{variable?.Identifier}\" x=\"{text.Position.X}\" y=\"{text.Position.Y}\" fill=\"{text.TextColor.ColorToString()}\" font-family=\"{text.Font}\" font-size=\"{text.FontSize}\">{text.Text}</text>");
            break;
        case FinalSquare square:
            SvgLines.Add($"<rect id=\"{variable?.Identifier}\" x=\"{square.TopLeft.X}\" y=\"{square.TopLeft.Y}\" width=\"{square.Length}\" height=\"{square.Length}\" fill=\"{square.FillColor.ColorToString()}\" fill-opacity=\"{square.FillColor.Alpha}\" stroke=\"{square.StrokeColor.ColorToString()}\" stroke-width=\"{square.Stroke}\" />");
            break;

        case FinalGroup group:
            SvgLines.Add($"<g id=\"{variable?.Identifier}\" transform=\"translate({group.Point.X}, {group.Point.Y})\">");
            GenerateSvg(group.Scope.vTable);
            SvgLines.Add("</g>");
            return;
        case FinalList list:
            for (int i = 0; i < list.Values.Count; i++)
            {
                GenerateLine(list.Values[i], variable);
            }
            return;
    }
}
}
