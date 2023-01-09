using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SvgCodeGen2;

public class SvgDocument
{
    public ISvgElement? Root { get; set; }

    public SvgDocument(ISvgElement? root)
    {
        Root = root;
    }

    public SvgDocument(string filename)
    {
        var doc = XDocument.Load(filename);

        doc.Descendants()
           .Attributes()
           .Where(x => x.IsNamespaceDeclaration)
           .Remove();

        XElement? root = doc.Root;

        if (root != null)
            Root = Utils.Deserialize(root);
    }

    public void Save(string filename)
    {
        var doc = ToXDocument();
        doc?.Save(filename);
    }

    public XDocument? ToXDocument()
    {
        if (Root == null)
            return null;
        var root = Utils.Serialize(Root);
        var doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            root
        );
        return doc;
    }

    public override string ToString()
    {
        var doc = ToXDocument();
        return doc == null ? string.Empty : doc.ToString();
    }

}
