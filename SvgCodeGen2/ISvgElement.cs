using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgCodeGen2;

public interface ISvgElement : ICloneable
{
    bool HasChilden { get; }
    bool AllowsNesting();
    bool CanGenerateValidSvg();
    XElement ToXElement(bool includeSvgNamespace);
}
