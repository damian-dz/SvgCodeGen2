using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class T : Command
    {
        public double X { get; set; }
        public double Y { get; set; }

        public T(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public T(List<string> tokens, bool isRelative = false)
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
            throw new NotImplementedException();
        }

        public override void Translate(double deltaX, double deltaY)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{(IsRelative ? 't' : 'T')}{X},{Y}";
        }
    }
}
