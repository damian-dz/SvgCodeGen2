using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SvgCodeGen2;

[XmlRoot("path")]
public partial class SvgPath : SvgElement
{
    [XmlAttribute("d")]
    public string? D;

    public void ArcTo(double rx, double ry, double xRot, bool large, bool sweep, double x, double y)
    {
        D += $" A{rx} {ry} {xRot} {Convert.ToInt32(large)} {Convert.ToInt32(sweep)} {x} {y}";
    }

    public void Close()
    {
        D += " Z";
    }

    public void LineTo(double x, double y)
    {
        D += $" L{x} {y}";
    }

    public void MoveTo(double x, double y)
    {
        string space = D == null ? string.Empty : " ";
        D += $"{space}M{x} {y}";
    }

    public List<Command> ParseToCommands()
    {
        if (string.IsNullOrWhiteSpace(D))
            throw new ArgumentException("The path string cannot be empty.");
        string d = D.Trim();
        if (char.ToUpper(d[0]) != 'M')
            throw new ArgumentException("The path string must start with a Move command.");
        int idxStart = 1;
        var commands = new List<Command>();
        var commandLetters = new char[] { 'M', 'L', 'A', 'C', 'Z' };
        for (int i = 1; i < d.Length; i++)
        {
            char dU = char.ToUpper(d[i]);
            if (commandLetters.Contains(dU) || i == d.Length - 1)
            {
                char c = d[idxStart - 1];
                string text = d[idxStart..i];
                List<string> tokens = Command.Parse(text);
                char cU = char.ToUpper(c);
                bool isRelative = char.IsLower(c);
                if (cU == 'M')
                {
                    if (tokens.Count % 2 == 0)
                    {
                        for (int j = 0; j < tokens.Count; j +=2)
                            commands.Add(new M(tokens.GetRange(j, 2), isRelative));
                    }
                }
                else if (cU == 'L')
                {
                    if (tokens.Count % 2 == 0)
                    {
                        for (int j = 0; j < tokens.Count; j += 2)
                            commands.Add(new L(tokens.GetRange(j, 2), isRelative));
                    }
                }
                else if (cU == 'C')
                {
                    if (tokens.Count % 6 == 0)
                    {
                        for (int j = 0; j < tokens.Count; j += 6)
                            commands.Add(new C(tokens.GetRange(j, 6), isRelative));
                    }
                }
                else if (cU == 'A')
                {
                    if (tokens.Count % 7 == 0)
                    {
                        for (int j = 0; j < tokens.Count; j += 7)
                            commands.Add(new A(tokens.GetRange(j, 7), isRelative));
                    }
                }
                if (dU == 'Z')
                    commands.Add(new Z(char.IsLower(d[i])));
                idxStart = i + 1;
            }
        }
        return commands;
    }

}
