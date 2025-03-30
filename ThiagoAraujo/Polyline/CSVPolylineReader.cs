using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThiagoAraujo.Interfaces;
using ThiagoAraujo.Models;

namespace ThiagoAraujo.Polyline
{
    // CSV implementation of the polyline reader.
    public class CSVPolylineReader : IPolylineReader
    {
        public List<Point> ReadPolyline(string filePath)
        {
            var polyline = new List<Point>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                // Split the line by comma, tab, or whitespace.
                string[] tokens = line.Split(new char[] { ',', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length >= 2 &&
                    double.TryParse(tokens[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double x) &&
                    double.TryParse(tokens[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double y))
                {
                    polyline.Add(new Point(x, y));
                }
                else
                {
                    Console.WriteLine($"Warning: Could not parse line: {line}");
                }
            }
            return polyline;
        }
    }
}
