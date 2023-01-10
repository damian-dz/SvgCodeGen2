using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class Q : Command
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Q(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public Q(List<string> tokens, bool isRelative = false)
        {
            IsRelative = isRelative;
            MapTokens(tokens);
        }

        private void MapTokens(List<string> tokens)
        {
            if (tokens is [var x1, var y1, var x, var y, ..])
            {
                X1 = double.Parse(x1);
                Y1 = double.Parse(y1);
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
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{(IsRelative ? 'q' : 'Q')}{X1},{Y1} {X},{Y}";
        }
    }
}
