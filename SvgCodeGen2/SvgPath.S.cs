using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class S : Command
    {
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public S(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public S(List<string> tokens, bool isRelative = false)
        {
            IsRelative = isRelative;
            MapTokens(tokens);
        }

        private void MapTokens(List<string> tokens)
        {
            if (tokens is [var x2, var y2, var x, var y, ..])
            {
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
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{(IsRelative ? 's' : 'S')}{X2},{Y2} {X},{Y}";
        }
    }
}
