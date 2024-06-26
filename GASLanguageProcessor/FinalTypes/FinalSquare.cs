﻿namespace GASLanguageProcessor.FinalTypes;

public class FinalSquare
{
    public FinalPoint TopLeft { get; set; }
    public FinalNum Length { get; set; }
    public FinalNum Stroke { get; set; }
    public FinalColor FillColor { get; set; }
    public FinalColor StrokeColor { get; set; }
    public FinalNum CornerRounding { get; set; }

    public FinalSquare(FinalPoint topLeft, float length, float stroke, FinalColor fillColor, FinalColor strokeColor, float cornerRounding)
    {
        TopLeft = topLeft;
        Length = new FinalNum(length);
        Stroke = new FinalNum(stroke);
        FillColor = fillColor;
        StrokeColor = strokeColor;
        CornerRounding = new FinalNum(cornerRounding);
    }
}