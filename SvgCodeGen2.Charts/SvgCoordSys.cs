using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvgCodeGen2.Charts;

[XmlRoot("g")]
public class SvgCoordSys : SvgGroup
{
    public SvgCoordSys() { }
    public SvgCoordSys(double xMin, double xMax, double yMin, double yMax, double xOrigin = 0, double yOrigin = 0, double lineWidth = 1)
    {
        double xRange = xMax - xMin;
        double arrowHeight = xRange / 100;
        double arrowWidth = arrowHeight / 2;


        if (IsInInterval(yOrigin, yMin, yMax))
        {
            var lineX = new SvgLine(xMin, yOrigin, xMax - arrowHeight, yOrigin);
            lineX.StrokeWidth= lineWidth;
            Add(lineX);
            var arrowX = new SvgPolygon(new double[] {
                xMax, yOrigin,
                xMax - arrowHeight, yOrigin - arrowWidth / 2,
                xMax - arrowHeight, yOrigin + arrowWidth / 2
            });
            arrowX.StrokeWidth = 0;
            Add(arrowX);
        }

        if (IsInInterval(xOrigin, xMin, xMax))
        {
            var lineY = new SvgLine(xOrigin, yMin + arrowHeight, xOrigin, yMax);
            lineY.StrokeWidth = lineWidth;
            Add(lineY);
            var arrowY = new SvgPolygon(new double[] {
                xOrigin, yMin,
                xOrigin + arrowWidth / 2, yMin + arrowHeight,
                xOrigin - arrowWidth / 2, yMin + arrowHeight
            });
            arrowY.StrokeWidth = 0;
            Add(arrowY);
        }
        
    }

    private static bool IsInInterval(double x, double min, double max)
        => x >= min && x <= max;
}
