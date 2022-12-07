using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SvgCodeGen2;

public abstract class SvgNestElement : SvgElementBase, ISvgElement
{
    public int Count => Elements.Count;

    public bool HasChilden => Elements.Count > 0;

    [XmlIgnore]
    public List<ISvgElement> Elements = new();


    public bool AllowsNesting()
    {
        return true;
    }

    public void Add(ISvgElement? element)
    {
        if (element != null)
            Elements.Add(element);
    }

    public bool CanGenerateValidSvg()
    {
        throw new NotImplementedException();
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public override XElement ToXElement(bool includeSvgNamespace = false)
    {
        var xElement = base.ToXElement(includeSvgNamespace);
        if (HasChilden)
        {
            foreach (var item in Elements)
            {
                var subXElement = item.ToXElement(includeSvgNamespace);
                xElement.Add(subXElement);
            }
        }
        return xElement;
    }

    public ISvgElement this[int i]
    {
        get
        { 
            return Elements[i];
        }
        set
        {
            Elements[i] = value;
        }
    }

}
