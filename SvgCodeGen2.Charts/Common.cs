using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SvgCodeGen2.Charts;

public struct Padding
{
    public double Left;
    public double Top;
    public double Right;
    public double Bottom;

    public Padding(double padding)
    {
        this.Left = padding;
        this.Top = padding;
        this.Right = padding;
        this.Bottom = padding;
    }
}

public struct Options
{
    public double LineWidth;
    public double GridStepX;
    public double GridStepY;
}