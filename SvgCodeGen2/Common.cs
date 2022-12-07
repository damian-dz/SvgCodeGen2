namespace SvgCodeGen2;

public enum Color
{
    AliceBlue = 0xF0F8FF,
    AntiqueWhite = 0xFAEBD7,
    Aqua = 0x00FFFF,
    AquaMarine = 0x7FFFD4,
    Azure = 0xF0FFFF,
    Beige = 0xF5F5DC,
    Bisque = 0xFFE4C4,
    Black = 0x000000,
    BlanchedAlmond = 0xFFEBCD,
    Blue = 0x0000FF,
    BlueViolet = 0x8A2BE2,
    Fuchsia = 0xFF00FF,
    Gray = 0x808080,
    Green = 0x008000,
    Lime = 0x00FF00,
    Maroon = 0x800000,
    Navy = 0x000080,
    Olive = 0x808000,
    Purple = 0x800080,
    Red = 0xFF0000,
    Silver = 0xC0C0C0,
    Teal = 0x008080,
    White = 0xFFFFFF,
    Yellow = 0xFFFF00
}

public struct Point
{
    public double X;
    public double Y;

    public Point(string coords)
    {
        var split = coords.Split(',');
        if (split is [var x, var y])
        {
            X = double.Parse(x);
            Y = double.Parse(y);
        }
    }

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point Rotate(Point pivot, double angle)
    {
        double px = X - pivot.X;
        double py = Y - pivot.Y;
        double s = Math.Sin(angle);
        double c = Math.Cos(angle);
        double xNew = px * c - py * s;
        double yNew = px * s + py * c;
        px = xNew + pivot.X;
        py = yNew + pivot.Y;
        return new Point(px, py);
    }

    public Point Translate(double x, double y)
    {
        return new Point(X + x, Y + y);
    }

    public string ToCompactString()
    {
        return $"{X},{Y}";
    }

    public override string ToString()
    {
        return $"x: {X}, y:{Y}";
    }

}