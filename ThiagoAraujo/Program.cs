using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ThiagoAraujo.Interfaces;
using ThiagoAraujo.Models;
using ThiagoAraujo.Polyline;

namespace FugroProgrammingExercise
{
    // Main program that ties everything together.
    class Program
    {
        static void Main(string[] args)
        {
            // Build the absolute file path from the output directory.
            string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input", "one.csv"));
            Console.WriteLine($"Looking for file: {filePath}");
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: The file was not found. Ensure 'one.csv' exists in the 'Polyline' folder in the project root and is copied to the output directory.");
                return;
            }

            // Dependency injection: using a CSV polyline reader.
            IPolylineReader polylineReader = new CSVPolylineReader();
            List<Point> polyline = polylineReader.ReadPolyline(filePath);
            if (polyline.Count < 2)
            {
                Console.WriteLine("Error: The polyline file must contain at least two valid points.");
                return;
            }

            // Read user input.
            Console.WriteLine("Enter Easting and Northing (separated by a space or comma):");
            string input = Console.ReadLine();
            string[] inputTokens = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (inputTokens.Length < 2 ||
                !double.TryParse(inputTokens[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double userX) ||
                !double.TryParse(inputTokens[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double userY))
            {
                Console.WriteLine("Invalid input. Please enter two numeric values.");
                return;
            }
            Point userPoint = new Point(userX, userY);

            // Dependency injection: using the station/offset calculator.
            IStationOffsetCalculator calculator = new StationOffsetCalculator();
            StationOffsetResult result = calculator.Compute(polyline, userPoint);

            // Output the results.
            Console.WriteLine($"\nComputed results:");
            Console.WriteLine($"Station: {result.Station:F2}");
            Console.WriteLine($"Offset: {result.Offset:F2}");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
