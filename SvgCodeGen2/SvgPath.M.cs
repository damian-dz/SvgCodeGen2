using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class M : Command
    {
        public double X { get; set; }
        public double Y { get; set; }

        public M(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public M(List<string> tokens, bool isRelative = false)
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

        public M(double x, double y)
        {
            X = x;
            Y = y;
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
            return $"{(IsRelative ? 'm' : 'M')}{X} {Y}";
        }

    }
}
