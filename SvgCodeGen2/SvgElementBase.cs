using System.ComponentModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SvgCodeGen2;

/// <summary>
/// SvgElementBase is the base class for most SVG elements.
/// </summary>
public class SvgElementBase
{
    [XmlIgnore]
    public string Tag { get; protected set; }

    [XmlAttribute("id")]
    public string? Id { get; set; }

    [XmlAttribute("stroke")]
    public string? Stroke { get; set; }

    [XmlAttribute("stroke-opacity"), DefaultValue(1)]
    public double StrokeOpacity { get; set; } = 1;

    [XmlAttribute("stroke-width"), DefaultValue(1)]
    public double StrokeWidth { get; set; } = 1;

    [XmlAttribute("stroke-linecap")]
    public string? StrokeLinecap { get; set; }

    [XmlAttribute("stroke-linejoin")]
    public string? StrokeLinejoin { get; set; }

    [XmlAttribute("stroke-dasharray")]
    public string? StrokeDashArray { get; set; }

    [XmlAttribute("fill")]
    public string? Fill { get; set; }

    [XmlAttribute("fill-opacity"), DefaultValue(1)]
    public double FillOpacity { get; set; } = 1;

    [XmlAttribute("style")]
    public string? Style { get; set; }

    [XmlAttribute("transform")]
    public string? Transform { get; set; }

    [XmlAttribute("transform-origin")]
    public string? TransformOrigin { get; set; }



    private static readonly XmlWriterSettings s_xmlWriterSettings = new()
    {
        Encoding = new UTF8Encoding(false), // don't include BOM
        OmitXmlDeclaration = true,
    };

    private static readonly XNamespace s_svgNs = "http://www.w3.org/2000/svg";

    public virtual XElement ToXElement(bool includeSvgNamespace = false)
    {
        Type type = GetType();
        var xmlSerializer = new XmlSerializer(type);
        using var stream = new MemoryStream();
        var ns = new XmlSerializerNamespaces();
        ns.Add("", null);
        var xmlWriter = XmlWriter.Create(stream, s_xmlWriterSettings);
        xmlSerializer.Serialize(xmlWriter, this, ns);
        string serialized = Encoding.UTF8.GetString(stream.ToArray());
        XElement xElement = XElement.Parse(serialized);
        if (includeSvgNamespace)
            xElement.Name = s_svgNs + xElement.Name.LocalName;

        return xElement;
    }

    public List<ParsedTransform> ParseTransforms()
    {
        if (string.IsNullOrWhiteSpace(Transform))
            throw new ArgumentException("The transform string cannot be empty.");

        var transforms = new List<ParsedTransform>();

        return transforms;
    }

    public void SetStroke(Color color)
    {
        Stroke = "#" + ((int)color).ToString("x6");
    }

    public override string ToString()
    {
        return ToXElement().ToString();
    }

    public abstract class ParsedTransform
    {
        
    }

}
