using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SvgCodeGen2;

public static class Utils
{
    public static XElement? Serialize(ISvgElement svgCurrent)
    {
        var svgCurrentBase = svgCurrent as SvgElementBase;
        XElement? xElement = svgCurrentBase?.ToXElement(true);
        //if (svgCurrent.AllowsNesting() && svgCurrent.HasChilden)
        //{
        //    if (svgCurrent is SvgNestElement svgNestCurrent)
        //    {
        //        foreach (var item in svgNestCurrent.Elements)
        //        {
        //            XElement? subXElement = Serialize(item);
        //            xElement?.Add(subXElement);
        //        }
        //    }
        //}
        return xElement;
    }

    public static ISvgElement? Deserialize(XElement xCurrent)
    {
        xCurrent.Name = xCurrent.Name.LocalName;
        var type = TypeForName(xCurrent.Name.LocalName);
        var rootAttribute = new XmlRootAttribute(xCurrent.Name.LocalName);
        var xmlSerializer = new XmlSerializer(type, rootAttribute);
        var xmlReader = xCurrent.CreateReader();
        var svgElement = xmlSerializer.Deserialize(xmlReader) as ISvgElement;
        if (svgElement == null)
            return null;

        if (svgElement.AllowsNesting() && xCurrent.HasElements)
        {
            if (svgElement is SvgNestElement svgNestElement)
            {
                foreach (var item in xCurrent.Elements())
                {
                    ISvgElement? subSvgElement = Deserialize(item);
                    svgNestElement.Add(subSvgElement);
                }
            }
        }
        return svgElement;
    }

    public static Type TypeForName(string name)
    {
        return name switch
        {
            "svg" => typeof(Svg),
            "circle" => typeof(SvgCircle),
            "line" => typeof(SvgLine),
            "path" => typeof(SvgPath),
            "polyline" => typeof(SvgPolyline),
            "polygon" => typeof(SvgPolygon),
            "g" => typeof(SvgGroup),
            _ => typeof(Type)
        };
    }
}