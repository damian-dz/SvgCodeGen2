using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class A : Command
    {
        public double Rx { get; set; }
        public double Ry { get; set; }
        public double XRot { get; set; }
        public bool Large { get; set; }
        public bool Sweep { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public A(double rx, double ry, double xRot, bool large, bool sweep, double x, double y)
        {
            Rx = rx;
            Ry = ry;
            XRot = xRot;
            Large = large;
            Sweep = sweep;
            X = x;
            Y = y;
        }

        public A(string text, bool isRelative = false)
        {
            IsRelative = isRelative;
            List<string> tokens = Parse(text);
            MapTokens(tokens);
        }

        public A(List<string> tokens, bool isRelative = false)
        {
            IsRelative = isRelative;
            MapTokens(tokens);
        }

        private void MapTokens(List<string> tokens)
        {
            if (tokens is [var rx, var ry, var xRot, var large, var sweep, var x, var y, ..])
            {
                Rx = double.Parse(rx);
                Ry = double.Parse(ry);
                XRot = double.Parse(xRot);
                Large = Convert.ToBoolean(int.Parse(large));
                Sweep = Convert.ToBoolean(int.Parse(sweep));
                X = double.Parse(x);
                Y = double.Parse(y);
            }
        }


        public override void Scale(double factor)
        {
            Rx *= factor;
            Ry *= factor;
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
            return $"{(IsRelative ? 'a' : 'A')}{Rx} {Ry} {XRot} {Convert.ToInt32(Large)} {Convert.ToInt32(Sweep)} {X} {Y}";
        }

    }
}
