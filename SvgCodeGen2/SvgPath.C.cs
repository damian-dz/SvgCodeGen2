using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class C : Command
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public C(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public C(List<string> tokens, bool isRelative = false)
        {
            IsRelative = isRelative;
            MapTokens(tokens);
        }

        private void MapTokens(List<string> tokens)
        {
            if (tokens is [var x1, var y1, var x2, var y2, var x, var y, ..])
            {
                X1 = double.Parse(x1);
                Y1 = double.Parse(y1);
                X2 = double.Parse(x2);
                Y2 = double.Parse(y2);
                X = double.Parse(x);
                Y = double.Parse(y);
            }
        }

        public override void Scale(double factor)
        {
            throw new NotImplementedException();
        }

        public override void Translate(double deltaX, double deltaY)
        {
            X1 += deltaX;
            Y1 += deltaY;
            X2 += deltaX;
            Y2 += deltaY;
            X += deltaX;
            Y += deltaY;
        }

        public override string ToString()
        {
            return $"{(IsRelative ? 'c' : 'C')}{X1},{Y1} {X2},{Y2} {X},{Y}";
        }
    }
}
