using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThiagoAraujo.Interfaces;
using ThiagoAraujo.Models;

namespace ThiagoAraujo.Polyline
{
    // Concrete implementation of the station and offset calculator.
    public class StationOffsetCalculator : IStationOffsetCalculator
    {
        public StationOffsetResult Compute(List<Point> polyline, Point userPoint)
        {
            double bestOffset = double.MaxValue;
            double bestStation = 0;
            double cumulativeDistance = 0;

            // Iterate over each segment of the polyline.
            for (int i = 0; i < polyline.Count - 1; i++)
            {
                Point a = polyline[i];
                Point b = polyline[i + 1];
                double segLength = Distance(a, b);
                if (segLength == 0)
                    continue;

                // Compute projection factor t.
                double t = ((userPoint.X - a.X) * (b.X - a.X) + (userPoint.Y - a.Y) * (b.Y - a.Y)) / (segLength * segLength);
                // Clamp t to the range [0, 1] so we only consider the segment.
                double clampedT = Math.Max(0, Math.Min(1, t));

                // Compute the projection point.
                Point projection = new Point(a.X + clampedT * (b.X - a.X), a.Y + clampedT * (b.Y - a.Y));

                // Compute distance from user point to the projection.
                double offset = Distance(userPoint, projection);
                double station = cumulativeDistance + clampedT * segLength;

                // Check if this is the best (smallest) offset.
                if (offset < bestOffset)
                {
                    bestOffset = offset;
                    bestStation = station;
                }

                // Update cumulative distance.
                cumulativeDistance += segLength;
            }

            return new StationOffsetResult { Station = bestStation, Offset = bestOffset };
        }

        /// <summary>
        /// Computes the Euclidean distance between two points.
        /// </summary>
        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}
