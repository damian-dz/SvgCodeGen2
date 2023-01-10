using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;
public partial class SvgPath
{
    public class V : Command
    {
        public double Y { get; set; }

        public V(double y)
        {
            Y = y;
        }

        public V(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public V(List<string> tokens, bool isRelative = false)
        {
            IsRelative = isRelative;
            MapTokens(tokens);
        }

        private void MapTokens(List<string> tokens)
        {
            if (tokens is [var y, ..])
            {
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
    }
}
