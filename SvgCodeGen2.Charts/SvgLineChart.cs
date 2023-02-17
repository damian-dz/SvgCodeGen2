using System;
using System.Transactions;
using System.Xml.Serialization;

namespace SvgCodeGen2.Charts;

[XmlRoot("svg")]
public class SvgLineChart : Svg
{
    public SvgLineChart() { }

    public SvgLineChart(IEnumerable<double> xData, IEnumerable<double> yData)
    {
        double xMin = xData.First();
        double xMax = xData.Last();

        double yMin = yData.Min();
        double yMax = yData.Max();

        double xRange = xMax - xMin;
        double xStep = xRange / 10;
        double yRange = yMax - yMin;
        double yStep = yRange / 10;

        var rectGrid = new SvgRectGrid(xMin, xMax, yMin, yMax, xStep, yStep);
        rectGrid.SetStroke(Color.Gray);
        rectGrid.StrokeWidth = xRange / 500;

        var group = new SvgGroup();

        group.Add(rectGrid);

        var polyline = new SvgPolyline(xData, yData);
        polyline.SetStroke(Color.Blue);
        polyline.StrokeWidth = xRange / 100;
        polyline.Fill = "none";
        group.Add(polyline);

        Add(group);

        Width = xRange.ToString();
        Height = yRange.ToString();

        Transform = "scale(100)";

        SetViewBox(xMin, yMin, xRange, yRange);
    }

    public SvgLineChart(IEnumerable<double> xData, IEnumerable<double> yData, double width, double height, Options options)
    {
        var xFitted = FitHorizontally(xData, options.Padding.Left, width - options.Padding.Right, out double xMin, out double xMax);
        var yFitted = FitVertically(yData, options.Padding.Bottom, height - options.Padding.Top, out double yMin, out double yMax);


        double xRange = xMax - xMin;
        double yRange = yMax - yMin;
        double xFactor = xRange / (width - options.Padding.Left - options.Padding.Right);
       // double xFactor = xRange / width;
        double yFactor = yRange / (height - options.Padding.Top - options.Padding.Bottom);

        var xGridData = GenerateValues(xMin, xMax, options.GridStepX);
        var yGridData = GenerateValues(yMin, yMax, options.GridStepY);

        double xGridMin = NearestGreaterMultiple(options.GridStepX, xMin) - xMin;
        double xGridMax = NearestLowerMultiple(options.GridStepX, xMax) - xMax;

        var xGridFitted = FitHorizontally(xGridData,
            options.Padding.Left + xGridMin / xFactor, width - options.Padding.Right + xGridMax / xFactor,
            out double _, out double _);

        double yGridMin = NearestGreaterMultiple(options.GridStepY, yMin) - yMin;
        double yGridMax = NearestLowerMultiple(options.GridStepY, yMax) - yMax;
        var yGridFitted = FitHorizontally(yGridData,
            options.Padding.Bottom + yGridMin / yFactor, height - options.Padding.Top + yGridMax / yFactor,
            out double _, out double _);
        var yGridMirrored = MirrorVertically(yGridFitted, height);

        var rectGrid = new SvgRectGrid(xGridFitted, yGridMirrored, 0, width, 0, height);
        rectGrid.SetStroke(Color.Gray);
        rectGrid.StrokeWidth = width / 1600;
        rectGrid.StrokeDashArray = options.GridDashArray;

        Add(rectGrid);

        if (options.IncludeAxes)
        {
            double xOrigin = Map(0, xMin, xMax, options.Padding.Left, width - options.Padding.Right);
            double yOrigin = height - Map(0, yMin, yMax, options.Padding.Bottom, height - options.Padding.Top);
            var coordSys = new SvgCoordSys(0, width, 0, height, xOrigin, yOrigin, width / 800);
            coordSys.SetStroke(Color.Black);
            coordSys.StrokeWidth = width / 800;
            Add(coordSys);
        }

        //var rectGrid = new SvgRectGrid(0, width, 0, height, width / 20, height / 20);
        //rectGrid.SetStroke(Color.Gray);
        //rectGrid.StrokeWidth = width / 1600;

        var yMirrored = MirrorVertically(yFitted, height);
        var polyline = new SvgPolyline(xFitted, yMirrored, 3)
        {
            StrokeWidth = options.LineWidth,
            Fill = "none"
        };
        polyline.SetStroke(Color.Blue);

        var group = new SvgGroup();
        group.Add(polyline);

        Add(group);

        var frame = new SvgRect(width, height)
        {
            StrokeWidth = width / 1000,
            Fill = "none"
        };
        frame.SetStroke(Color.Black);

        Add(frame);

        const double fontXFactor = 2.78;
        const double fontYFactor = 1.73;
        const double spaceXFactor = 5.33;
        double charWidth = options.TickFontSize / fontXFactor;
        double charHeight = options.TickFontSize / fontYFactor;
        double spaceWidth = options.TickFontSize / spaceXFactor;



        var xTicks = new SvgGroup();
        var xTickLabels = new SvgGroup();
        xTickLabels.Fill = "black";
        for (int i = 0; i < xGridFitted.Count(); i++)
        {
            double xTickX = xGridFitted.ElementAt(i);
            var xTick = new SvgLine(xTickX, height, xTickX, height + height / 25);
            xTick.StrokeWidth = width / 1600;
            xTick.SetStroke(Color.Black);
            xTicks.Add(xTick);
            string xTickLabelText = xGridData.ElementAt(i).ToString();
            double xTickLabelWidth = charWidth * xTickLabelText.Length + spaceWidth * (xTickLabelText.Length - 1);
            double xTickLabelXShift = xTickLabelText.StartsWith("-") ?
                xTickLabelWidth / 2 + charWidth + fontYFactor * 1.25 :
                xTickLabelWidth / 2 + fontYFactor * 1.25;
            var xTickLabel = new SvgText(xTickX - xTickLabelXShift,
                height + height / 25 + charHeight * 1.5, xTickLabelText);
            xTickLabel.FontFamily = options.TickFontFamily;
            xTickLabel.FontSize = options.TickFontSize.ToString();
            xTickLabels.Add(xTickLabel);
        }
        Add(xTicks);
        Add(xTickLabels);


        var yTicks = new SvgGroup();
        var yTickLabels = new SvgGroup();
        yTickLabels.Fill = "black";


        for (int i = 0; i < yGridMirrored.Count(); i++)
        {
            double yTickY = yGridMirrored.ElementAt(i);
            var yTick = new SvgLine(-height / 25, yTickY, 0, yTickY);
            yTick.StrokeWidth = width / 1600;
            yTick.SetStroke(Color.Black);
            yTicks.Add(yTick);
            string yTickLabelText = yGridData.ElementAt(i).ToString();
            double yTickLabelWidth = charWidth * yTickLabelText.Length + spaceWidth * (yTickLabelText.Length - 1);
            var yTickLabel = new SvgText(-height / 19 - yTickLabelWidth, yTickY + charHeight / 2, yTickLabelText);
            yTickLabel.FontFamily = options.TickFontFamily;
            yTickLabel.FontSize = options.TickFontSize.ToString();
            yTickLabels.Add(yTickLabel);
        }
        Add(yTicks);
        Add(yTickLabels);

        SetViewBox(-width / 20, -height / 20, width + width / 10, height + height / 5);
        Width = width.ToString();
        Height = height.ToString();
    }

    public static double Map(double v, double oldMin, double oldMax, double newMin, double newMax)
        => (v - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;


    private static IEnumerable<double> GenerateValues(double min, double max, double step)
    {
        double x = Math.Ceiling(min / step) * step - step;
        while (x <= max - step)
        {
            x += step;
            yield return x;
        }
    }

    private static IEnumerable<double> MirrorVertically(IEnumerable<double> values, double height)
    {
        foreach (double value in values)
        {
            double mirroredValue = height - value;
            yield return mirroredValue;
        }
    }

    private static IEnumerable<double> FitHorizontally(IEnumerable<double> values, double newMin, double newMax, out double oldMin, out double oldMax)
    {
        oldMin = values.First();
        oldMax = values.Last();
        var fitted = FitToInterval(values, oldMin, oldMax, newMin, newMax);
        return fitted;
    }

    private static IEnumerable<double> FitVertically(IEnumerable<double> values, double newMin, double newMax, out double oldMin, out double oldMax)
    {
        oldMin = values.Min();
        oldMax = values.Max();
        var fitted = FitToInterval(values, oldMin, oldMax, newMin, newMax);
        return fitted;
    }

    private static IEnumerable<double> FitToInterval(IEnumerable<double> values, double oldMin, double oldMax, double newMin, double newMax)
    {
        double oldRange = oldMax - oldMin;
        double newRange = newMax - newMin;

        foreach (double value in values)
        {
            double normalizedValue = (value - oldMin) / oldRange;
            double fittedValue = normalizedValue * newRange + newMin;
            yield return fittedValue;
        }
    }

    private static double NearestGreaterMultiple(double num, double target)
         => Math.Ceiling(target / num) * num;

    private static double NearestLowerMultiple(double num, double target)
        => Math.Floor(target / num) * num;


}