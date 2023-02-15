using System.Xml;
using System.Xml.Serialization;

namespace SvgCodeGen2;


[XmlRoot("svg", Namespace = "http://www.w3.org/2000/svg")]
public class Svg : SvgNestElement
{
    [XmlAttribute("version")]
    public string? Version;
    [XmlAttribute("baseProfile")]
    public string? BaseProfile;
    [XmlAttribute("width")]
    public string? Width;
    [XmlAttribute("height")]
    public string? Height;
    [XmlAttribute("viewBox")]
    public string? ViewBox;

    public Svg()
    {
        
    }

    public Svg(double width, double height)
    {
        Width = width.ToString();
        Height = height.ToString();
    }

    public void SetSize(double width, double height)
    {
        Width = width.ToString();
        Height = height.ToString();
    }

    public void SetWidth(double width) 
        => Width = width.ToString();

    public void SetHeight(double height)
        => Height = height.ToString();

    public void SetPixelSize(double width, double height)
    {
        Width = $"{width}px";
        Height = $"{height}px";
    }

    public void SetPixelWidth(double width)
        => Width = $"{width}px";

    public void SetPixelHeight(double height)
        => Height = $"{height}px";

    public void SetViewBox(double x, double y, double width, double height)
        => ViewBox = $"{x} {y} {width} {height}";

}
