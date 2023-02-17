namespace SvgCodeGen2.Charts;

public struct Padding
{
    public double Left;
    public double Top;
    public double Right;
    public double Bottom;

    public Padding(double padding)
    {
        Left = padding;
        Top = padding;
        Right = padding;
        Bottom = padding;
    }

    public Padding(double left, double top, double right, double bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
}

public struct Margin
{
    public double Left;
    public double Top;
    public double Right;
    public double Bottom;

    public Margin(double padding)
    {
        Left = padding;
        Top = padding;
        Right = padding;
        Bottom = padding;
    }

    public Margin(double left, double top, double right, double bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }
}

public struct Options
{
    public double LineWidth;
    public double GridStepX;
    public double GridStepY;
    public string? GridDashArray;
    public bool IncludeAxes;
    public string? TickFontFamily;
    public float TickFontSize = 12;
    public Padding Padding;
    public Margin Margin;

    public Options()
    {
    }
}