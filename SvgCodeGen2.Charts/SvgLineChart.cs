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

    public SvgLineChart(IEnumerable<double> xData, IEnumerable<double> yData, double width, double height, Options options, Padding padding)
    {
        var xFitted = FitHorizontally(xData, padding.Left, width - padding.Right, out double xMin, out double xMax);
        var yFitted = FitVertically(yData, padding.Bottom, height - padding.Top, out double yMin, out double yMax);


        double xRange = xMax- xMin;
        double yRange = yMax- yMin;
        double xFactor = xRange / (width - padding.Left - padding.Right);
        double yFactor = yRange / (width - padding.Top - padding.Bottom);

        var xGridData = GenerateValues(xMin, xMax, options.GridStepX);
        var yGridData = GenerateValues(yMin, yMax, options.GridStepY);

        var xGridFitted = FitHorizontally(xGridData, padding.Left, width - padding.Right, out double _, out double _);
        var yGridFitted = FitHorizontally(yGridData, padding.Bottom, height - padding.Top, out double _, out double _);
        var yGridMirrored = MirrorVertically(yGridFitted, height);

     
        var rectGrid = new SvgRectGrid(xGridFitted, yGridMirrored, 0, width, 0, height);
        rectGrid.SetStroke(Color.Gray);
        rectGrid.StrokeWidth = width / 1600;

        Add(rectGrid);

        double xOrigin = Map(0, xMin, xMax, padding.Left, width - padding.Right);
        double yOrigin = height - Map(0, yMin, yMax, padding.Bottom, height - padding.Top);
        var coordSys = new SvgCoordSys(0, width, 0, height, xOrigin, yOrigin, width / 800);
        coordSys.SetStroke(Color.Black);
        coordSys.StrokeWidth = width / 800;

        //var rectGrid = new SvgRectGrid(0, width, 0, height, width / 20, height / 20);
        //rectGrid.SetStroke(Color.Gray);
        //rectGrid.StrokeWidth = width / 1600;

        Add(coordSys);

        var yMirrored = MirrorVertically(yFitted, height);
        var polyline = new SvgPolyline(xFitted, yMirrored, 3);


        polyline.SetStroke(Color.Blue);
        polyline.StrokeWidth = options.LineWidth;
        polyline.Fill = "none";

        var group = new SvgGroup();
        group.Add(polyline);

        Add(group);

       

        Width = width.ToString();
        Height = height.ToString();
    }

    public static double Map(double v, double oldMin, double oldMax, double newMin, double newMax)
        => (v - oldMin) * (newMax - newMin) / (oldMax - oldMin) + newMin;


    private static IEnumerable<double> GenerateValues(double min, double max, double step)
    {
        double x = Math.Ceiling(min / step) * step;
        while (x <= max)
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




}