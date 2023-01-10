using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class H : Command
    {
        public double X { get; set; }

        public H(double x)
        {
            X = x;
        }

        public H(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public H(List<string> tokens, bool isRelative = false)
        {
            IsRelative = isRelative;
            MapTokens(tokens);
        }

        private void MapTokens(List<string> tokens)
        {
            if (tokens is [var x, ..])
            {
                X = double.Parse(x);
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
    }
}
