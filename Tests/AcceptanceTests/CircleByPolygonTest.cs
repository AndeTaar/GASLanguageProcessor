namespace Tests.AcceptanceTests;
using Xunit;
public class CircleByPolygonTest
{
    [Fact]
    public void CircleByPolygonTest()
    {
        var gas = @"Color white = Color{red=255, green=255, blue=255, alpha=1 };
            Canvas canvas = Canvas {width=500, height=500, backgroundColor=white};

            num g = white.green;

            Color red = Color{red=255, green=0, blue=0, alpha=white.alpha };

            Color blue = Color{red=0, green=0, blue=255, alpha=white.alpha };

            num Cos(num angleInDegrees) {
                num angle = angleInDegrees * 3.14159 / 180;
                num result = 1;
                num term = 1;
                for (num i = 1; i <= 10; i+=1) {
                    term *= -angle * angle / ((2 * i) * (2 * i - 1));
                    result += term;
                }
                return result;
            }

            num Sin(num angleInDegrees) {
                num angle = angleInDegrees * 3.14159 / 180;
                num result = angle;
                num term = angle;
                for (num i = 1; i <= 10; i+=1) {
                    term *= -angle * angle / ((2 * i + 1) * (2 * i));
                    result += term;
                }
                return result;
            }

            Circle c;
            Polygon[] polygons = new Polygon[200];
            num polyIndex = 0;
            Point center = Point{x=canvas.width/2, y=canvas.height/2};

            void createCircleDiagram(num radius, Point center, num percentage){
                Point[] points = new Point[500];
                num pointIndex = 0;
                num entireCircle = 360;
                entireCircle = entireCircle * percentage / 100;

                for (num theta = 0; theta <= entireCircle; theta += 1)
                {
                    num x2 = center.x + radius * Sin(theta)*-1;
                    num y = center.y + radius * Cos(theta)*-1;
                    points[pointIndex] = Point{x=x2, y=y};
                    pointIndex++;
                }

                points[pointIndex] = Point{x=center.x, y=center.y};
                pointIndex++;
                c = Circle{
                    center=Point{
                        x=center.x,
                        y=center.y
                    },
                    radius=50,
                    stroke=0,
                    color=blue,
                    strokeColor=red
                };

                Polygon poly = Polygon{points=points, stroke=0, color=red, strokeColor=red};
                polygons[polyIndex] = poly;
                polyIndex++;
            }

            num radius = 50;

            createCircleDiagram(radius, center, 75);

            ";



    }
}
