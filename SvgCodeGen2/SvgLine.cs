using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvgCodeGen2;

[XmlRoot("line")]
public sealed class SvgLine : SvgElement
{
    [XmlAttribute("x1")]
    public double X1;
    [XmlAttribute("y1")]
    public double Y1;
    [XmlAttribute("x2")]
    public double X2;
    [XmlAttribute("y2")]
    public double Y2;

    public SvgLine()
    {

    }

    public SvgLine(double x1, double y1, double x2, double y2)
    {
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
    }
}
