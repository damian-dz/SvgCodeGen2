using System.Xml.Serialization;

namespace SvgCodeGen2;

[XmlRoot("circle")]
public class SvgCircle : SvgElement
{
    [XmlAttribute("cx")]
    public double Cx;
    [XmlAttribute("cy")]
    public double Cy;
    [XmlAttribute("r")]
    public double R;

    public SvgCircle()
    {

    }

    public SvgCircle(double cx, double cy, double r)
    {
        Cx = cx;
        Cy = cy;
        R = r;
    }
}
