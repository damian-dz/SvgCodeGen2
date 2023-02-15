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

    [XmlIgnore]
    public List<Command>? Commands { get; set; }

    public void ArcTo(double rx, double ry, double xRot, bool large, bool sweep, double x, double y, bool isRelative = false)
    {
        D += $"{ (isRelative ? 'a' : 'A')}{rx} {ry},{xRot} {Convert.ToInt32(large)},{Convert.ToInt32(sweep)} {x},{y}";
    }

    public void CurveTo(double x1, double y1, double x2, double y2, double x, double y, bool isRelative = false)
    {
        D += $"{ (isRelative ? 'c' : 'C')}{x1} {y1},{x2} {y2},{x} {y}";
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
        string space = string.IsNullOrWhiteSpace(D) ? string.Empty : " ";
        D += $"{space}M{x} {y}";
    }

    public void Translate(double dx, double dy)
    {
        if (Commands is null)
            return;
        foreach (var command in Commands)
        {
            command.Translate(dx, dy);
        }
    }

    public void Format()
    {
        DataToCommands();
        CommandsToData();
    }

    public void CommandsToData()
    {
        if (Commands == null)
            return;
        D = string.Empty;
        foreach (var command in Commands)
            D += command.ToString();
    }

    public void DataToCommands()
    {
        if (string.IsNullOrWhiteSpace(D))
            throw new ArgumentException("The path string cannot be empty.");
        string d = D.Trim();
        if (char.ToUpper(d[0]) != 'M')
            throw new ArgumentException("The path string must start with a Move command.");
        int idxStart = 1;
        Commands = new List<Command>();
        var commandLetters = new char[] { 'M', 'L', 'H', 'V', 'C', 'S', 'Q', 'T', 'A', 'Z' };
        for (int i = 1; i < d.Length; i++)
        {
            char dU = char.ToUpper(d[i]);
            if (commandLetters.Contains(dU) || i == d.Length - 1)
            {
                char c = d[idxStart - 1];
                int idxEnd = i == d.Length - 1 ? dU == 'Z' ? i : d.Length : i;
                string text = d[idxStart..idxEnd];
                List<string> tokens = Command.Parse(text);
                char cU = char.ToUpper(c);
                bool isRelative = char.IsLower(c);
                if (tokens.Count > 0)
                {
                    if (cU == 'M')
                    {
                        if (tokens.Count % 2 == 0)
                        {
                            for (int j = 0; j < tokens.Count; j += 2)
                                Commands.Add(new M(tokens.GetRange(j, 2), isRelative));
                        }
                    }
                    else if (cU == 'L')
                    {
                        if (tokens.Count % 2 == 0)
                        {
                            for (int j = 0; j < tokens.Count; j += 2)
                                Commands.Add(new L(tokens.GetRange(j, 2), isRelative));
                        }
                    }
                    else if (cU == 'H')
                    {
                        for (int j = 0; j < tokens.Count; j++)
                            Commands.Add(new H(tokens[j], isRelative));
                    }
                    else if (cU == 'V')
                    {
                        for (int j = 0; j < tokens.Count; j++)
                            Commands.Add(new V(tokens[j], isRelative));
                    }
                    else if (cU == 'A')
                    {
                        if (tokens.Count % 7 == 0)
                        {
                            for (int j = 0; j < tokens.Count; j += 7)
                                Commands.Add(new A(tokens.GetRange(j, 7), isRelative));
                        }
                    }
                    else if (cU == 'C')
                    {
                        if (tokens.Count % 6 == 0)
                        {
                            for (int j = 0; j < tokens.Count; j += 6)
                                Commands.Add(new C(tokens.GetRange(j, 6), isRelative));
                        }
                    }
                    else if (cU == 'S')
                    {
                        if (tokens.Count % 4 == 0)
                        {
                            for (int j = 0; j < tokens.Count; j += 4)
                                Commands.Add(new C(tokens.GetRange(j, 4), isRelative));
                        }
                    }
                    else if (cU == 'Q')
                    {
                        if (tokens.Count % 4 == 0)
                        {
                            for (int j = 0; j < tokens.Count; j += 4)
                                Commands.Add(new Q(tokens.GetRange(j, 4), isRelative));
                        }
                    }
                    else if (cU == 'T')
                    {
                        if (tokens.Count % 2 == 0)
                        {
                            for (int j = 0; j < tokens.Count; j += 2)
                                Commands.Add(new T(tokens.GetRange(j, 2), isRelative));
                        }
                    }
                }
                if (dU == 'Z')
                    Commands.Add(new Z(char.IsLower(d[i])));
                idxStart = i + 1;
            }
        }
    }

}
