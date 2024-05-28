using Antlr4.Runtime.Misc;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class SvgGenerator
{
    public ArrayList<string> SvgLines = new();

    public ArrayList<string> GenerateSvg(VariableTable vTable)
    {
        if (vTable.Variables.ContainsKey("canvas"))
        {
            var canvas = vTable.Variables["canvas"];
            vTable.Variables.Remove("canvas");
            GenerateLine(canvas.ActualValue, canvas);
        }

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
            SvgLines.Add($"<circle id=\"{variable?.Identifier}\" cx=\"{circle.Center.X}\" cy=\"{circle.Center.Y}\" r=\"{circle.Radius}\" fill=\"{circle.FillColors.ColorToString()}\" fill-opacity=\"{circle.FillColors.Alpha}\" stroke=\"{circle.StrokeColors.ColorToString()}\" stroke-width=\"{circle.Stroke}\" />");
            break;
        case FinalLine line:
            SvgLines.Add($"<line id=\"{variable?.Identifier}\" x1=\"{line.Start.X}\" y1=\"{line.Start.Y}\" x2=\"{line.End.X}\" y2=\"{line.End.Y}\" stroke=\"{line.StrokeColors.ColorToString()}\" stroke-width=\"{line.Stroke}\" />");
            break;
        case FinalRectangle rectangle:
            SvgLines.Add($"<rect id=\"{variable?.Identifier}\" x=\"{rectangle.TopLeft.X}\" y=\"{rectangle.TopLeft.Y}\" width=\"{rectangle.Width}\" height=\"{rectangle.Height}\" fill=\"{rectangle.FillColors.ColorToString()}\" fill-opacity=\"{rectangle.FillColors.Alpha}\" rx=\"{rectangle.CornerRounding}\" stroke=\"{rectangle.StrokeColors.ColorToString()}\" stroke-width=\"{rectangle.Stroke}\" />");
            break;
        case FinalSegLine segLine:
            SvgLines.Add($"<line id=\"{variable?.Identifier}\" x1=\"{segLine.Start.X}\" y1=\"{segLine.Start.Y}\" x2=\"{segLine.End.X}\" y2=\"{segLine.End.Y}\" stroke=\"{segLine.StrokeColors.ColorToString()}\" stroke-width=\"{segLine.Stroke}\" />");
            break;
        case FinalArrow arrow:
            SvgLines.Add($"<line id=\"{variable?.Identifier}\" x1=\"{arrow.Start.X}\" y1=\"{arrow.Start.Y}\" x2=\"{arrow.End.X}\" y2=\"{arrow.End.Y}\" stroke=\"{arrow.StrokeColors.ColorToString()}\" stroke-width=\"{arrow.Stroke}\" />");
            SvgLines.Add($"<polygon id=\"{variable?.Identifier}\" points=\"{arrow.ArrowHead.ToString()}\" fill=\"{arrow.ArrowHead.Colors.ColorToString()}\" fill-opacity=\"{arrow.ArrowHead.Colors.Alpha}\" stroke=\"{arrow.ArrowHead.StrokeColors.ColorToString()}\" stroke-width=\"{arrow.ArrowHead.Stroke}\" />");
            break;
        case FinalEllipse ellipse:
            SvgLines.Add($"<ellipse id=\"{variable?.Identifier}\" cx=\"{ellipse.Center.X}\" cy=\"{ellipse.Center.Y}\" rx=\"{ellipse.RadiusX}\" ry=\"{ellipse.RadiusY}\" fill=\"{ellipse.Colors.ColorToString()}\" stroke=\"{ellipse.StrokeColor.ColorToString()}\" stroke-width=\"{ellipse.Stroke}\" />");
            break;
        case FinalText text:
            SvgLines.Add($"<text id=\"{variable?.Identifier}\" x=\"{text.Position.X}\" y=\"{text.Position.Y}\" fill=\"{text.TextColors.ColorToString()}\" font-family=\"{text.Font}\" font-weight=\"{text.FontWeight}\" font-size=\"{text.FontSize}\">{text.Text}</text>");
            break;
        case FinalSquare square:
            SvgLines.Add($"<rect id=\"{variable?.Identifier}\" x=\"{square.TopLeft.X}\" y=\"{square.TopLeft.Y}\" width=\"{square.Length}\" height=\"{square.Length}\" fill=\"{square.FillColors.ColorToString()}\" fill-opacity=\"{square.FillColors.Alpha}\" rx=\"{square.CornerRounding}\" stroke=\"{square.StrokeColors.ColorToString()}\" stroke-width=\"{square.Stroke}\" />");
            break;
        case FinalLinearGradient linearGradient:
            double angle = linearGradient.Rotation.Value;
            double radians = angle * (Math.PI / 180.0);

            double x2 = Math.Cos(radians) * 100;
            double y2 = Math.Sin(radians) * 100;
            SvgLines.Add($"<linearGradient id=\"{variable?.Identifier}\" x1=\"0%\" y1=\"0%\" x2=\"{x2}%\" y2=\"{y2}%\">");
            for (int i = 0; i < linearGradient.ColorList.Values.Count; i++)
            {
                SvgLines.Add($"<stop offset=\"{linearGradient.PercentagesList.Values[i]}%\" stop-color=\"{((FinalRGB) linearGradient.ColorList.Values[i]).ColorToString()}\" />");
            }
            SvgLines.Add("</linearGradient>");
            linearGradient.Id = variable?.Identifier;
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
        case FinalPolygon polygon:
            SvgLines.Add($"<polygon id=\"{variable?.Identifier}\" points=\"{polygon.Points.ToString()}\" fill=\"{polygon.Colors.ColorToString()}\" fill-opacity=\"{polygon.Colors.Alpha}\" stroke=\"{polygon.StrokeColors.ColorToString()}\" stroke-width=\"{polygon.Stroke}\" />");
            break;

    }
}
}
