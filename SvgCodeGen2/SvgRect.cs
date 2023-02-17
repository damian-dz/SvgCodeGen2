using System.Xml.Serialization;

namespace SvgCodeGen2;

[XmlRoot("rect")]
public class SvgRect : SvgElement
{

    [XmlAttribute("x")]
    public double X;
    [XmlAttribute("y")]
    public double Y;
    [XmlAttribute("width")]
    public double Width;
    [XmlAttribute("height")]
    public double Height;

    public SvgRect()
    {

    }

    /// <summary>
    /// Contructs a rectangle with the specified width and height.
    /// </summary>
    /// <param name="width">rectangle width</param>
    /// <param name="height">rectangle height</param>
    public SvgRect(double width, double height)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Contructs a rectangle with the specified width and height at the given position.
    /// </summary>
    /// <param name="x">left top corner X-coordinate</param>
    /// <param name="y">left top corner Y-coordinate</param>
    /// <param name="width">rectangle width</param>
    /// <param name="height">rectangle height</param>
    public SvgRect(double x, double y, double width, double height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

}
