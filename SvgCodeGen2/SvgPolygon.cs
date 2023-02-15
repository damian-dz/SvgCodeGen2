using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvgCodeGen2;

[XmlRoot("polygon")]
public class SvgPolygon : SvgPolyline
{

    public SvgPolygon() { }

    public SvgPolygon(IEnumerable<double> data, int? numDecimalPlaces = null) 
        : base(data, numDecimalPlaces)
    {

    }

    public SvgPolygon(IEnumerable<double> xData, IEnumerable<double> yData, int? numDecimalPlaces = null)
        : base(xData, yData, numDecimalPlaces)
    {

    }

}