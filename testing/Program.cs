using SvgCodeGen2;
using SvgCodeGen2.Charts;

Action? Example1 = () =>
{
    var circle = new SvgCircle(100, 100, 90);
    var line = new SvgLine(0, 0, 200, 200)
    {
        StrokeWidth = 10,
        StrokeLinecap = "round"
    };
    line.SetStroke(Color.Black);
    var g = new SvgGroup();
    g.Add(circle);
    g.Add(line);
    var svg = new Svg(200, 200);
    svg.SetViewBox(-10, -10, 220, 220);
    svg.Add(g);
    var svgDoc = new SvgDocument(svg);
    svgDoc.Save("example1.svg");
};

Example1();
