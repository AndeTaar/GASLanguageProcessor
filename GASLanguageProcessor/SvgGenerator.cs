using Antlr4.Runtime.Misc;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.FinalTypes.Colors;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class SvgGenerator
{
    public ArrayList<string> SvgLines = new();

    public ArrayList<string> GenerateSvg(Store sto)
    {
        for(int i = 0; i < sto.Values.Count; i++)
        {
            var variable = sto.LookUp(i);
            GenerateLine(variable, i, new VarEnv());
        }
        SvgLines.Add("</svg>");

        return SvgLines;
    }

    public void GenerateLine(object obj, int index, VarEnv varEnv)
    {
        if (obj is not FinalType)
        {
            return;
        }

        FinalType finalType = (FinalType)obj;
        if (finalType.Fields?.Values.Count != 0 && finalType.Fields != null)
        {
            foreach (var ft in finalType.Fields.Values)
            {
                GenerateLine(ft, -1, varEnv);
            }
        }

        switch (obj)
        {
            case FinalCanvas canvas:
                SvgLines.Add(
                    $"<svg width=\"{canvas.Width}\" height=\"{canvas.Height}\" style=\"background-color: {canvas.BackgroundColor.ColorToString()}; object-fit: contain;\" xmlns=\"http://www.w3.org/2000/svg\">");
                break;
            case FinalCircle circle:
                SvgLines.Add(
                    $"<circle id=\"{varEnv.GetIdentifier(index)}\" cx=\"{circle.Center.X}\" cy=\"{circle.Center.Y}\" r=\"{circle.Radius}\" fill=\"{circle.FillColor.ColorToString()}\" fill-opacity=\"{circle.FillColor.Alpha}\" stroke=\"{circle.StrokeColor.ColorToString()}\" stroke-width=\"{circle.Stroke}\" />");
                break;
            case FinalLine line:
                SvgLines.Add(
                    $"<line id=\"{varEnv.GetIdentifier(index)}\" x1=\"{line.Start.X}\" y1=\"{line.Start.Y}\" x2=\"{line.End.X}\" y2=\"{line.End.Y}\" stroke=\"{line.StrokeColor.ColorToString()}\" stroke-width=\"{line.Stroke}\" />");
                break;
            case FinalRectangle rectangle:
                SvgLines.Add(
                    $"<rect id=\"{varEnv.GetIdentifier(index)}\" x=\"{rectangle.TopLeft.X}\" y=\"{rectangle.TopLeft.Y}\" width=\"{rectangle.Width}\" height=\"{rectangle.Height}\" fill=\"{rectangle.FillColor.ColorToString()}\" fill-opacity=\"{rectangle.FillColor.Alpha}\" rx=\"{rectangle.CornerRounding}\" stroke=\"{rectangle.StrokeColor.ColorToString()}\" stroke-width=\"{rectangle.Stroke}\" />");
                break;
            case FinalSegLine segLine:
                SvgLines.Add(
                    $"<line id=\"{varEnv.GetIdentifier(index)}\" x1=\"{segLine.Start.X}\" y1=\"{segLine.Start.Y}\" x2=\"{segLine.End.X}\" y2=\"{segLine.End.Y}\" stroke=\"{segLine.StrokeColor.ColorToString()}\" stroke-width=\"{segLine.Stroke}\" />");
                break;
            case FinalArrow arrow:
                SvgLines.Add(
                    $"<line id=\"{varEnv.GetIdentifier(index)}\" x1=\"{arrow.Start.X}\" y1=\"{arrow.Start.Y}\" x2=\"{arrow.End.X}\" y2=\"{arrow.End.Y}\" stroke=\"{arrow.StrokeColor.ColorToString()}\" stroke-width=\"{arrow.Stroke}\" />");
                SvgLines.Add(
                    $"<polygon id=\"{varEnv.GetIdentifier(index)}\" points=\"{arrow.ArrowHead}\" fill=\"{arrow.ArrowHead.Color.ColorToString()}\" fill-opacity=\"{arrow.ArrowHead.Color.Alpha}\" stroke=\"{arrow.ArrowHead.StrokeColor.ColorToString()}\" stroke-width=\"{arrow.ArrowHead.Stroke}\" />");
                break;
            case FinalEllipse ellipse:
                SvgLines.Add(
                    $"<ellipse id=\"{varEnv.GetIdentifier(index)}\" cx=\"{ellipse.Center.X}\" cy=\"{ellipse.Center.Y}\" rx=\"{ellipse.RadiusX}\" ry=\"{ellipse.RadiusY}\" fill=\"{ellipse.Color.ColorToString()}\" stroke=\"{ellipse.StrokeColor.ColorToString()}\" stroke-width=\"{ellipse.Stroke}\" />");
                break;
            case FinalTriangle triangle:
                SvgLines.Add(
                    $"<polygon id=\"{varEnv.GetIdentifier(index)}\" points=\"{triangle.Points}\" fill=\"{triangle.Color.ColorToString()}\" fill-opacity=\"{triangle.Color.Alpha}\" stroke=\"{triangle.StrokeColor.ColorToString()}\" stroke-width=\"{triangle.Stroke}\" />");
                break;

            case FinalText text:
                SvgLines.Add(
                    $"<text id=\"{varEnv.GetIdentifier(index)}\" x=\"{text.Position.X}\" y=\"{text.Position.Y}\" xml:space=\"preserve\" fill=\"{text.TextColor.ColorToString()}\" dominant-baseline=\"middle\" text-anchor=\"middle\" font-family=\"{text.Font}\" font-weight=\"{text.FontWeight}\" font-size=\"{text.FontSize}\">{text.Text}</text>");
                break;
            case FinalSquare square:
                SvgLines.Add(
                    $"<rect id=\"{varEnv.GetIdentifier(index)}\" x=\"{square.TopLeft.X}\" y=\"{square.TopLeft.Y}\" width=\"{square.Length}\" height=\"{square.Length}\" fill=\"{square.FillColor.ColorToString()}\" fill-opacity=\"{square.FillColor.Alpha}\" rx=\"{square.CornerRounding}\" stroke=\"{square.StrokeColor.ColorToString()}\" stroke-width=\"{square.Stroke}\" />");
                break;
            case FinalLinearGradient linearGradient:
                double angle = linearGradient.Rotation.Value;
                double radians = angle * (Math.PI / 180.0);

                double x2 = Math.Cos(radians) * 100;
                double y2 = Math.Sin(radians) * 100;
                SvgLines.Add($"<linearGradient id=\"{linearGradient.Id}\" x1=\"0%\" y1=\"0%\" x2=\"{x2}%\" y2=\"{y2}%\">");
                for (int i = 0; i < linearGradient.Colors.Values.Length; i++)
                {
                    SvgLines.Add($"<stop offset=\"{linearGradient.Stops.Values[i]}%\" stop-color=\"{((FinalColors) linearGradient.Colors.Values[i]).ColorToString()}\" />");
                }
                SvgLines.Add("</linearGradient>");
                break;

            case FinalGroup group:
                SvgLines.Add(
                    $"<g id=\"{varEnv.GetIdentifier(index)}\" transform=\"translate({group.Point.X}, {group.Point.Y})\">");
                GenerateSvg(group.Store);
                SvgLines.Add("</g>");
                return;
            case FinalList list:
                for (var i = 0; i < list.Values.Length; i++) GenerateLine(list.Values[i], -1, varEnv);
                return;
            case FinalPolygon polygon:
                SvgLines.Add(
                    $"<polygon id=\"{varEnv.GetIdentifier(index)}\" points=\"{polygon.Points}\" fill=\"{polygon.Color.ColorToString()}\" fill-opacity=\"{polygon.Color.Alpha}\" stroke=\"{polygon.StrokeColor.ColorToString()}\" stroke-width=\"{polygon.Stroke}\" />");
                break;
        }
    }
}
