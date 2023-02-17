using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SvgCodeGen2;

[XmlRoot("polyline")]
public class SvgPolyline : SvgElement
{

    [XmlAttribute("points")]
    public string? Points;

    public SvgPolyline() { }

    public SvgPolyline(IEnumerable<double> data, int? numDecimalPlaces = null)
    {
        if (numDecimalPlaces == null)
            Points = string.Join(' ', data);
        else
            Points = string.Join(' ',
                data.Select(d => d.ToString($"0.{new string('#', numDecimalPlaces.Value)}",
                CultureInfo.InvariantCulture)));
    }

    public SvgPolyline(IEnumerable<double> xData, IEnumerable<double> yData, int? numDecimalPlaces = null)
    {
        IEnumerable<double> data = xData.Zip(yData, (a, b) => new double[] { a, b }).SelectMany(d => d);
        if (numDecimalPlaces == null)
            Points = string.Join(' ', data);
        else
            Points = string.Join(' ',
                data.Select(d => d.ToString($"0.{new string('#', numDecimalPlaces.Value)}",
                CultureInfo.InvariantCulture)));
    }

    public void AddPoint(double x, double y)
    {
        if (Points == null)
        {
            Points = "";
            Points += x + " " + y;
        }
        else
        {
            Points += " " + x + " " + y;
        }
    }


}
