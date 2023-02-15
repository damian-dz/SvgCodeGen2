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
    public SvgCoordSys(double xMin, double xMax, double yMin, double yMax)
    {
        var lineX = new SvgLine(xMin, 0, xMax, 0);
        var lineY = new SvgLine(0, yMin, 0, yMax);

        Add(lineX);
        Add(lineY);

        double xRange = xMax - xMin;
        double arrowWidth = xRange / 100;
        double arrowHeight = arrowWidth / 2;

        var arrowX = new SvgPolygon(new double[] { xMax - 2, 0, xMax - arrowWidth - 2, -arrowHeight / 2, xMax - arrowWidth - 2, arrowHeight / 2 });
        var arrowY = new SvgPolygon(new double[] { 0, yMin + 2, arrowHeight / 2, yMin + arrowWidth + 2, -arrowHeight / 2, yMin + arrowWidth + 2 });

        Add(arrowX);
        Add(arrowY);
    }
}
