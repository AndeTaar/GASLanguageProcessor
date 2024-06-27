using GASLanguageProcessor.AST.Expressions;
using GASLanguageProcessor.FinalTypes;
using GASLanguageProcessor.FinalTypes.Colors;
using GASLanguageProcessor.TableType;

namespace GASLanguageProcessor;

public class RecordEvaluator
{

    public bool hasCanvas = false;
    public Store EvaluateRecords(Store store)
    {
        Store recordStore = new();
        int rsIndex = 1;
        foreach (var value in store.Values)
        {
            if (value.Value is FinalList || value.Value is FinalRecord)
            {
                recordStore.Bind(rsIndex, EvaluateRecord((FinalType)value.Value, store));
                rsIndex++;
            }
        }

        if (!hasCanvas)
        {
            recordStore.Bind(0, new FinalCanvas(100, 100, new FinalColor(255, 255, 255, 1)));
        }

        return recordStore;
    }

    public object? EvaluateList(FinalList finalList, Store store)
    {
        var list = finalList.Values;
        var evaluatedList = new List<object>();
        foreach (var item in list)
        {
            if (item is FinalRecord)
            {
                evaluatedList.Add(EvaluateRecord((FinalRecord)item, store));
            }
            else
            {
                evaluatedList.Add(item);
            }
        }

        return new FinalList(evaluatedList.ToArray());
    }

    public object? EvaluateRecord(FinalType finalType, Store store)
    {
        if(finalType == null)
            return null;

        FinalRecord finalRecord = null;
        if (finalType is FinalList)
        {
            return EvaluateList((FinalList)finalType, store);
        }
        if (finalType is FinalRecord)
        {
            finalRecord = (FinalRecord)finalType;
        }

        var dictionary = finalRecord?.Fields;

        if (finalRecord?.Fields != null)
        {
            foreach (var field in finalRecord.Fields)
            {
                if (field.Value is FinalRecord)
                {
                    finalRecord.Fields[field.Key] = EvaluateRecord((FinalRecord)field.Value, store);
                }
            }
        }

        switch (finalRecord?.FinalRecordType)
        {
            case "Canvas":
                dictionary.TryGetValue("width", out var widthObj);
                dictionary.TryGetValue("height", out var heightObj);
                dictionary.TryGetValue("backgroundColor", out var backgroundColorObj);
                backgroundColorObj = EvaluateRecord(backgroundColorObj as FinalType, store);
                var width = widthObj != null ? (float)widthObj : 0.0f;
                var height = heightObj != null ? (float)heightObj : 0.0f;
                var backgroundColor = backgroundColorObj != null ? (FinalColors)backgroundColorObj : new FinalColor(0, 0, 0, 1);
                hasCanvas = true;
                return new FinalCanvas(width, height, backgroundColor) { Id=finalRecord.Id, Fields = dictionary };;

            case "Circle":
                dictionary.TryGetValue("center", out var centerObj);
                centerObj = EvaluateRecord(centerObj as FinalType, store);
                dictionary.TryGetValue("radius", out var radiusObj);
                dictionary.TryGetValue("stroke", out var strokeObj);
                dictionary.TryGetValue("color", out var colorObj);
                colorObj = EvaluateRecord(colorObj as FinalType, store);
                dictionary.TryGetValue("strokeColor", out var strokeColorObj);
                strokeColorObj = EvaluateRecord(strokeColorObj as FinalType, store);
                var center = centerObj != null ? (FinalPoint)centerObj : new FinalPoint(0, 0);
                var radius = radiusObj != null ? (float)radiusObj : 1.0f;
                var stroke = strokeObj != null ? (float)strokeObj : 1.0f;
                var color = colorObj != null ? (FinalColors)colorObj : new FinalColor(0, 0, 0, 1);
                var strokeColor = strokeColorObj != null ? (FinalColors)strokeColorObj : new FinalColor(0, 0, 0, 1);
                return new FinalCircle(center, radius, stroke, color, strokeColor) { Id=finalRecord.Id, Fields = dictionary };;

            case "Rectangle":
                dictionary.TryGetValue("topLeft", out var topLeftObj);
                topLeftObj = EvaluateRecord(topLeftObj as FinalType, store);
                dictionary.TryGetValue("bottomRight", out var bottomRightObj);
                bottomRightObj = EvaluateRecord(bottomRightObj as FinalType, store);
                dictionary.TryGetValue("stroke", out strokeObj);
                dictionary.TryGetValue("color", out colorObj);
                colorObj = EvaluateRecord(colorObj as FinalType, store);
                dictionary.TryGetValue("strokeColor", out strokeColorObj);
                strokeColorObj = EvaluateRecord(strokeColorObj as FinalType, store);
                dictionary.TryGetValue("rounding", out var roundingObj);
                var topLeft = topLeftObj != null ? (FinalPoint)topLeftObj : new FinalPoint(0, 0);
                var bottomRight = bottomRightObj != null ? (FinalPoint)bottomRightObj : new FinalPoint(1, 1);
                stroke = strokeObj != null ? (float)strokeObj : 1.0f;
                color = colorObj != null ? (FinalColors)colorObj : new FinalColor(0, 0, 0, 1);
                strokeColor = strokeColorObj != null ? (FinalColors)strokeColorObj : new FinalColor(0, 0, 0, 1);
                var rounding = roundingObj != null ? (float)roundingObj : 0.0f;
                return new FinalRectangle(topLeft, bottomRight, stroke, color, strokeColor, rounding)
                    { Id=finalRecord.Id, Fields = dictionary };;

            case "Point":
                dictionary.TryGetValue("x", out var xObj);
                dictionary.TryGetValue("y", out var yObj);
                var x = xObj != null ? (float)xObj : 0.0f;
                var y = yObj != null ? (float)yObj : 0.0f;
                return new FinalPoint(x, y) { Id=finalRecord.Id, Fields = dictionary };;

            case "Color":
                dictionary.TryGetValue("red", out var redObj);
                dictionary.TryGetValue("green", out var greenObj);
                dictionary.TryGetValue("blue", out var blueObj);
                dictionary.TryGetValue("alpha", out var alphaObj);
                var red = redObj != null ? (float)redObj : 0.0f;
                var green = greenObj != null ? (float)greenObj : 0.0f;
                var blue = blueObj != null ? (float)blueObj : 0.0f;
                var alpha = alphaObj != null ? (float)alphaObj : 1.0f;
                return new FinalColor(red, green, blue, alpha) { Id=finalRecord.Id, Fields = dictionary };;

            case "LinearGradient":
                dictionary.TryGetValue("colors", out var colorsObj);
                colorsObj = EvaluateRecord(colorsObj as FinalList, store);
                dictionary.TryGetValue("stops", out var stopsObj);
                stopsObj = EvaluateRecord(stopsObj as FinalList, store);
                dictionary.TryGetValue("rotation", out var rotationObj);
                dictionary.TryGetValue("alpha", out var alphaLinearObj);
                return new FinalLinearGradient((float)alphaLinearObj, (float)rotationObj, (FinalList)colorsObj, (FinalList)stopsObj) { Id=finalRecord.Id, Fields = dictionary };

            case "Ellipse":
                dictionary.TryGetValue("center", out var ellipseCenterObj);
                ellipseCenterObj = EvaluateRecord(ellipseCenterObj as FinalType, store);
                dictionary.TryGetValue("radiusX", out var ellipseRadiusXObj);
                dictionary.TryGetValue("radiusY", out var ellipseRadiusYObj);
                dictionary.TryGetValue("stroke", out var ellipseStrokeObj);
                ellipseStrokeObj = EvaluateRecord(ellipseStrokeObj as FinalType, store);
                dictionary.TryGetValue("color", out var ellipseColorObj);
                ellipseColorObj = EvaluateRecord(ellipseColorObj as FinalType, store);
                dictionary.TryGetValue("strokeColor", out var ellipseStrokeColorObj);
                ellipseStrokeColorObj = EvaluateRecord(ellipseStrokeColorObj as FinalType, store);
                var ellipseCenter = ellipseCenterObj != null ? (FinalPoint)ellipseCenterObj : new FinalPoint(0, 0);
                var ellipseRadiusX = ellipseRadiusXObj != null ? (float)ellipseRadiusXObj : 1.0f;
                var ellipseRadiusY = ellipseRadiusYObj != null ? (float)ellipseRadiusYObj : 1.0f;
                var ellipseStroke = ellipseStrokeObj != null ? (float)ellipseStrokeObj : 1.0f;
                var ellipseColor = ellipseColorObj != null ? (FinalColors)ellipseColorObj : new FinalColor(0, 0, 0, 1);
                var ellipseStrokeColor = ellipseStrokeColorObj != null
                    ? (FinalColors)ellipseStrokeColorObj
                    : new FinalColor(0, 0, 0, 1);
                return new FinalEllipse(ellipseCenter, ellipseRadiusX, ellipseRadiusY, ellipseStroke, ellipseColor,
                    ellipseStrokeColor) { Id=finalRecord.Id, Fields = dictionary };;

            case "Triangle":
                dictionary.TryGetValue("point1", out var point1Obj);
                dictionary.TryGetValue("point2", out var point2Obj);
                dictionary.TryGetValue("point3", out var point3Obj);
                var point1 = point1Obj != null ? (FinalPoint) EvaluateRecord(point1Obj as FinalType, store) : new FinalPoint(0, 0);
                var point2 = point2Obj != null ? (FinalPoint) EvaluateRecord(point2Obj as FinalType, store) : new FinalPoint(1, 1);
                var point3 = point3Obj != null ? (FinalPoint) EvaluateRecord(point3Obj as FinalType, store) : new FinalPoint(2, 2);
                var points = new FinalList ([ point1, point2, point3 ]);
                dictionary.TryGetValue("stroke", out var triangleStrokeObj);
                triangleStrokeObj = EvaluateRecord(triangleStrokeObj as FinalType, store);
                dictionary.TryGetValue("color", out var triangleColorObj);
                triangleColorObj = EvaluateRecord(triangleColorObj as FinalType, store);
                dictionary.TryGetValue("strokeColor", out var triangleStrokeColorObj);
                triangleStrokeColorObj = EvaluateRecord(triangleStrokeColorObj as FinalType, store);
                var triangleStroke = triangleStrokeObj != null ? (float)triangleStrokeObj : 1.0f;
                var triangleColor =
                    triangleColorObj != null ? (FinalColors)triangleColorObj : new FinalColor(0, 0, 0, 1);
                var triangleStrokeColor = triangleStrokeColorObj != null
                    ? (FinalColors)triangleStrokeColorObj
                    : new FinalColor(0, 0, 0, 1);
                return new FinalTriangle(points, triangleStroke, triangleColor, triangleStrokeColor)
                    { Id=finalRecord.Id, Fields = dictionary };;

            case "Polygon":
                dictionary.TryGetValue("points", out var polygonPointsObj);
                polygonPointsObj = EvaluateRecord(polygonPointsObj as FinalType, store);
                dictionary.TryGetValue("stroke", out var polygonStrokeObj);
                dictionary.TryGetValue("color", out var polygonColorObj);
                polygonColorObj = EvaluateRecord(polygonColorObj as FinalType, store);
                dictionary.TryGetValue("strokeColor", out var polygonStrokeColorObj);
                polygonStrokeColorObj = EvaluateRecord(polygonStrokeColorObj as FinalType, store);
                var polygonPoints = polygonPointsObj != null ? (FinalList)polygonPointsObj : null;
                var polygonStroke = polygonStrokeObj != null ? (float)polygonStrokeObj : 1.0f;
                var polygonColor = polygonColorObj != null ? (FinalColors)polygonColorObj : new FinalColor(0, 0, 0, 1);
                var polygonStrokeColor = polygonStrokeColorObj != null
                    ? (FinalColors) polygonStrokeColorObj
                    : new FinalColor(0, 0, 0, 1);
                return new FinalPolygon(polygonPoints, polygonStroke, polygonColor, polygonStrokeColor)
                    { Id=finalRecord.Id, Fields = dictionary };;

            case "SegLine":
                dictionary.TryGetValue("start", out var linePoint1Obj);
                linePoint1Obj = EvaluateRecord(linePoint1Obj as FinalType, store);
                dictionary.TryGetValue("end", out var linePoint2Obj);
                linePoint2Obj = EvaluateRecord(linePoint2Obj as FinalType, store);
                dictionary.TryGetValue("stroke", out var lineStrokeObj);
                dictionary.TryGetValue("color", out var lineColorObj);
                lineColorObj = EvaluateRecord(lineColorObj as FinalType, store);
                var linePoint1 = linePoint1Obj != null ? (FinalPoint)linePoint1Obj : new FinalPoint(0, 0);
                var linePoint2 = linePoint2Obj != null ? (FinalPoint)linePoint2Obj : new FinalPoint(1, 1);
                var lineStroke = lineStrokeObj != null ? (float)lineStrokeObj : 1.0f;
                var lineColor = lineColorObj != null ? (FinalColors)lineColorObj : new FinalColor(0, 0, 0, 1);
                return new FinalLine(linePoint1, linePoint2, lineStroke, lineColor) { Id=finalRecord.Id, Fields = dictionary };;

            case "Arrow":
                dictionary.TryGetValue("start", out var arrowStartObj);
                arrowStartObj = EvaluateRecord(arrowStartObj as FinalType, store);
                dictionary.TryGetValue("end", out var arrowEndObj);
                arrowEndObj = EvaluateRecord(arrowEndObj as FinalType, store);
                dictionary.TryGetValue("stroke", out var arrowStrokeObj);
                dictionary.TryGetValue("color", out var arrowColorObj);
                arrowColorObj = EvaluateRecord(arrowColorObj as FinalType, store);
                var arrowStart = arrowStartObj != null ? (FinalPoint)arrowStartObj : new FinalPoint(0, 0);
                var arrowEnd = arrowEndObj != null ? (FinalPoint)arrowEndObj : new FinalPoint(1, 1);
                var arrowStroke = arrowStrokeObj != null ? (float)arrowStrokeObj : 1.0f;
                var arrowColor = arrowColorObj != null ? (FinalColors)arrowColorObj : new FinalColor(0, 0, 0, 1);
                return new FinalArrow(arrowStart, arrowEnd, arrowStroke, arrowColor) { Id=finalRecord.Id, Fields = dictionary };;

            case "Text":
                dictionary.TryGetValue("point", out var textPositionObj);
                textPositionObj = EvaluateRecord(textPositionObj as FinalType, store);
                dictionary.TryGetValue("content", out var contentObj);
                dictionary.TryGetValue("color", out var textColorObj);
                textColorObj = EvaluateRecord(textColorObj as FinalType, store);
                dictionary.TryGetValue("font", out var fontObj);
                dictionary.TryGetValue("size", out var fontSizeObj);
                dictionary.TryGetValue("weight", out var fontWeightObj);
                var textPosition = textPositionObj != null ? (FinalPoint)textPositionObj : new FinalPoint(0, 0);
                var content = contentObj != null ? (string)contentObj : string.Empty;
                var textColor = textColorObj != null ? (FinalColors)textColorObj : new FinalColor(0, 0, 0, 1);
                var font = fontObj != null ? (string)fontObj : "Arial";
                var fontSize = fontSizeObj != null ? (float)fontSizeObj : 12.0f;
                var fontWeight = fontWeightObj != null ? (float)fontWeightObj : 400.0f;
                return new FinalText(content, textPosition, font, fontSize, fontWeight, textColor)
                    { Id=finalRecord.Id, Fields = dictionary };;

            case "Square":
                dictionary.TryGetValue("topLeft", out var squareTopLeftObj);
                squareTopLeftObj = EvaluateRecord(squareTopLeftObj as FinalType, store);
                dictionary.TryGetValue("side", out var squareLengthObj);
                dictionary.TryGetValue("stroke", out var squareStrokeObj);
                dictionary.TryGetValue("color", out var squareColorObj);
                squareColorObj = EvaluateRecord(squareColorObj as FinalType, store);
                dictionary.TryGetValue("strokeColor", out var squareStrokeColorObj);
                squareStrokeColorObj = EvaluateRecord(squareStrokeColorObj as FinalType, store);
                dictionary.TryGetValue("rounding", out var squareRoundingObj);
                var squareTopLeft = squareTopLeftObj != null ? (FinalPoint)squareTopLeftObj : new FinalPoint(0, 0);
                var squareLength = squareLengthObj != null ? (float)squareLengthObj : 1.0f;
                var squareStroke = squareStrokeObj != null ? (float)squareStrokeObj : 1.0f;
                var squareColor = squareColorObj != null ? (FinalColors)squareColorObj : new FinalColor(0, 0, 0, 1);
                var squareStrokeColor = squareStrokeColorObj != null
                    ? (FinalColors)squareStrokeColorObj
                    : new FinalColor(0, 0, 0, 1);
                var squareRounding = squareRoundingObj != null ? (float)squareRoundingObj : 0.0f;
                return new FinalSquare(squareTopLeft, squareLength, squareStroke, squareColor, squareStrokeColor,
                    squareRounding) { Id=finalRecord.Id, Fields = dictionary };;
        }

        return finalType;
    }
}
