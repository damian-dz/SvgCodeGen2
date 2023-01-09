using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class L : Command
    {
        public double X { get; set; }
        public double Y { get; set; }

        public L(double x, double y)
        {
            X = x;
            Y = y;
        }

        public L(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public L(List<string> tokens, bool isRelative = false)
        {
            IsRelative = isRelative;
            MapTokens(tokens);
        }

        private void MapTokens(List<string> tokens)
        {
            if (tokens is [var x, var y, ..])
            {
                X = double.Parse(x);
                Y = double.Parse(y);
            }
        }

        public override void Scale(double factor)
        {
            X *= factor;
            Y *= factor;
        }

        public override void Translate(double deltaX, double deltaY)
        {
            X += deltaX;
            Y += deltaY;
        }

        public override string ToString()
        {
            return $"{(IsRelative ? 'l' : 'L')}{X} {Y}";
        }

    }
}
