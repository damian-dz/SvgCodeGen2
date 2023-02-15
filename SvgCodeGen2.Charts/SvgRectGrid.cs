using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvgCodeGen2.Charts;

[XmlRoot("g")]
public class SvgRectGrid : SvgGroup
{
    public SvgRectGrid() { }

    public SvgRectGrid(double xMin, double xMax, double yMin, double yMax, double xStep, double yStep)
    {
        if (xMax <= xMin || yMax <= yMin)
            throw new ArgumentException("Specified range invalid.");

        double x = Math.Ceiling(xMin / xStep) * xStep;
        while (x <= xMax)
        {
            Add(new SvgLine(x, yMax, x, yMin));
            x += xStep;
        }

        double y = Math.Ceiling(yMin / yStep) * yStep;
        while (y <= yMax)
        {
            Add(new SvgLine(xMin, y, xMax, y));
            y += yStep;
        }

    }

    public SvgRectGrid(IEnumerable<double> xData, IEnumerable<double> yData, double xMin, double xMax, double yMin, double yMax)
    {
        foreach (double x in xData)
            Add(new SvgLine(x, yMax, x, yMin));
        foreach (double y in yData)
            Add(new SvgLine(xMin, y, xMax, y));
    }


}
