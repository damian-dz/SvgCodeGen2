using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2;

public partial class SvgPath
{
    public class Z : Command
    {
        public Z(bool isRelative = false)
        {
            IsRelative = isRelative;
        }

        public override void Scale(double factor)
        {
            return;
        }

        public override void Translate(double deltaX, double deltaY)
        {
            return;
        }

        public override string ToString()
        {
            return $"{(IsRelative ? 'z' : 'Z')}";
        }

    }
}
