SvgCodeGen2 is the successor of [SvgCodeGen](https://github.com/damian-dz/SvgCodeGen), a .NET library intended for programmatic generation and manipulation of SVG files, by exploiting .NET's built-in XML serialization and deserialization. It provides fine-grained control over the generated XML output, and, unlike many WYSIWYG programs, it tends not to insert superfluous tags or attributes.

## Examples

A simple SVG file can be created as follows:

```csharp
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
```

The above should produce the following XML code:
```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<svg width="200" height="200" viewBox="-10 -10 220 220" xmlns="http://www.w3.org/2000/svg">
  <g>
    <circle cx="100" cy="100" r="90" />
    <line stroke="#000000" stroke-width="10" stroke-linecap="round" x1="0" y1="0" x2="200" y2="200" />
  </g>
</svg>
```

Apart from the ``Svg`` element itself, the elements by default will not support explicitly defined size units (px, %, etc.). Implementing that would add unnecessary complexity to the code, likely slowing down the library, for no noticeable benefit.