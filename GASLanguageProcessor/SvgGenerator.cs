using Antlr4.Runtime.Misc;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class SvgGenerator
{
    public ArrayList<string> SvgLines = new();

    public ArrayList<string> GenerateSvg(VarEnv varEnv, Store sto)
    {
        if (varEnv.Variables.ContainsKey("canvas"))
        {
            var canvasIndex = varEnv.Variables["canvas"];
            var canvas = sto.LookUp(canvasIndex) as FinalCanvas;
            varEnv.Variables.Remove("canvas");
            GenerateLine(canvas, 0, varEnv);
        }

        foreach (int index in varEnv.Variables.Values)
        {
            var variable = sto.LookUp(index);
            GenerateLine(variable, index, varEnv);
        }

        return SvgLines;
    }





   public void GenerateLine(Object obj, int index, VarEnv varEnv)
   {
    switch (obj)
    {
        case FinalCanvas canvas:
            SvgLines.Add($"<svg width=\"{canvas.Width}\" height=\"{canvas.Height}\" style=\"background-color: {canvas.BackgroundColor.ColorToString()}\" xmlns=\"http://www.w3.org/2000/svg\">");
            break;
        case FinalCircle circle:
            SvgLines.Add($"<circle id=\"{varEnv.GetIdentifier(index)}\" cx=\"{circle.Center.X}\" cy=\"{circle.Center.Y}\" r=\"{circle.Radius}\" fill=\"{circle.FillColor.ColorToString()}\" fill-opacity=\"{circle.FillColor.Alpha}\" stroke=\"{circle.StrokeColor.ColorToString()}\" stroke-width=\"{circle.Stroke}\" />");
            break;
        case FinalLine line:
            SvgLines.Add($"<line id=\"{varEnv.GetIdentifier(index)}\" x1=\"{line.Start.X}\" y1=\"{line.Start.Y}\" x2=\"{line.End.X}\" y2=\"{line.End.Y}\" stroke=\"{line.StrokeColor.ColorToString()}\" stroke-width=\"{line.Stroke}\" />");
            break;
        case FinalRectangle rectangle:
            SvgLines.Add($"<rect id=\"{varEnv.GetIdentifier(index)}\" x=\"{rectangle.TopLeft.X}\" y=\"{rectangle.TopLeft.Y}\" width=\"{rectangle.Width}\" height=\"{rectangle.Height}\" fill=\"{rectangle.FillColor.ColorToString()}\" fill-opacity=\"{rectangle.FillColor.Alpha}\" rx=\"{rectangle.CornerRounding}\" stroke=\"{rectangle.StrokeColor.ColorToString()}\" stroke-width=\"{rectangle.Stroke}\" />");
            break;
        case FinalSegLine segLine:
            SvgLines.Add($"<line id=\"{varEnv.GetIdentifier(index)}\" x1=\"{segLine.Start.X}\" y1=\"{segLine.Start.Y}\" x2=\"{segLine.End.X}\" y2=\"{segLine.End.Y}\" stroke=\"{segLine.StrokeColor.ColorToString()}\" stroke-width=\"{segLine.Stroke}\" />");
            break;
        case FinalArrow arrow:
            SvgLines.Add($"<line id=\"{varEnv.GetIdentifier(index)}\" x1=\"{arrow.Start.X}\" y1=\"{arrow.Start.Y}\" x2=\"{arrow.End.X}\" y2=\"{arrow.End.Y}\" stroke=\"{arrow.StrokeColor.ColorToString()}\" stroke-width=\"{arrow.Stroke}\" />");
            SvgLines.Add($"<polygon id=\"{varEnv.GetIdentifier(index)}\" points=\"{arrow.ArrowHead.ToString()}\" fill=\"{arrow.ArrowHead.Color.ColorToString()}\" fill-opacity=\"{arrow.ArrowHead.Color.Alpha}\" stroke=\"{arrow.ArrowHead.StrokeColor.ColorToString()}\" stroke-width=\"{arrow.ArrowHead.Stroke}\" />");
            break;
        case FinalEllipse ellipse:
            SvgLines.Add($"<ellipse id=\"{varEnv.GetIdentifier(index)}\" cx=\"{ellipse.Center.X}\" cy=\"{ellipse.Center.Y}\" rx=\"{ellipse.RadiusX}\" ry=\"{ellipse.RadiusY}\" fill=\"{ellipse.Color.ColorToString()}\" stroke=\"{ellipse.StrokeColor.ColorToString()}\" stroke-width=\"{ellipse.Stroke}\" />");
            break;
        case FinalText text:
            SvgLines.Add($"<text id=\"{varEnv.GetIdentifier(index)}\" x=\"{text.Position.X}\" y=\"{text.Position.Y}\" fill=\"{text.TextColor.ColorToString()}\" font-family=\"{text.Font}\" font-weight=\"{text.FontWeight}\" font-size=\"{text.FontSize}\">{text.Text}</text>");
            break;
        case FinalSquare square:
            SvgLines.Add($"<rect id=\"{varEnv.GetIdentifier(index)}\" x=\"{square.TopLeft.X}\" y=\"{square.TopLeft.Y}\" width=\"{square.Length}\" height=\"{square.Length}\" fill=\"{square.FillColor.ColorToString()}\" fill-opacity=\"{square.FillColor.Alpha}\" rx=\"{square.CornerRounding}\" stroke=\"{square.StrokeColor.ColorToString()}\" stroke-width=\"{square.Stroke}\" />");
            break;

        /**case FinalGroup group:
            SvgLines.Add($"<g id=\"{varEnv.GetIdentifier(index)}\" transform=\"translate({group.Point.X}, {group.Point.Y})\">");
            GenerateSvg(group.vTable, group.Scope.stoTable);
            SvgLines.Add("</g>");
            return;*/
        case FinalList list:
            for (int i = 0; i < list.Values.Count; i++)
            {
                GenerateLine(list.Values[i], -1, varEnv);
            }
            return;
        case FinalPolygon polygon:
            SvgLines.Add($"<polygon id=\"{varEnv.GetIdentifier(index)}\" points=\"{polygon.Points.ToString()}\" fill=\"{polygon.Color.ColorToString()}\" fill-opacity=\"{polygon.Color.Alpha}\" stroke=\"{polygon.StrokeColor.ColorToString()}\" stroke-width=\"{polygon.Stroke}\" />");
            break;

    }
}
}
