using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvgCodeGen2;

[XmlRoot("polyline")]
public sealed class SvgPolyline : SvgElement
{


    [XmlAttribute("points")]
    public string? Points;
}
