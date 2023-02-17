using System.Xml.Serialization;

namespace SvgCodeGen2;

[XmlRoot("text")]
public class SvgText : SvgElement
{
    [XmlAttribute("x")]
    public double X;
    [XmlAttribute("y")]
    public double Y;
    [XmlAttribute("font-family")]
    public string? FontFamily;
    [XmlAttribute("font-size")]
    public string? FontSize;
    [XmlText]
    public string? Text { get; set; }

    public SvgText()
    {

    }

    public SvgText(double x, double y, string text)
    {
        X = x;
        Y = y;
        Text = text;
    }

    public SvgText(string text)
    {
        Text = text;
    }


}
