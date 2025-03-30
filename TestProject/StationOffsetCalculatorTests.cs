using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using FugroProgrammingExercise;
using ThiagoAraujo.Models;
using ThiagoAraujo.Polyline;

namespace FugroProgrammingExercise.Tests
{

    [TestFixture]
    public class StationOffsetCalculatorTests
    {
        [Test]
        public void Compute_StraightLinePointDirectlyAbove_ReturnsCorrectStationAndOffset()
        {
            // Arrange: Create a straight-line polyline from (0,0) to (10,0).
            List<Point> polyline = new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0)
            };

            // A point directly above the midpoint.
            Point userPoint = new Point(5, 5);
            var calculator = new StationOffsetCalculator();

            // Act
            StationOffsetResult result = calculator.Compute(polyline, userPoint);

            // Assert: Midpoint is at station 5; offset is 5 (vertical distance).
            Assert.That(result.Station, Is.EqualTo(5).Within(0.001));
            Assert.That(result.Offset, Is.EqualTo(5).Within(0.001));
        }

        [Test]
        public void Compute_PointAtEndpoint_ReturnsZeroOffset()
        {
            // Arrange: Create a polyline.
            List<Point> polyline = new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(20, 0)
            };

            // A point exactly on the first endpoint.
            Point userPoint = new Point(0, 0);
            var calculator = new StationOffsetCalculator();

            // Act
            StationOffsetResult result = calculator.Compute(polyline, userPoint);

            // Assert: The station should be 0 and the offset should be 0.
            Assert.That(result.Station, Is.EqualTo(0).Within(0.001));
            Assert.That(result.Offset, Is.EqualTo(0).Within(0.001));
        }

        [Test]
        public void Compute_PointClosestToSecondSegment_ReturnsCorrectValues()
        {
            // Arrange: Create a polyline with two segments: (0,0) -> (10,0) and (10,0) -> (10,10).
            List<Point> polyline = new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10)
            };

            // A point near the second segment: (12, 5) should project onto (10,5).
            Point userPoint = new Point(12, 5);
            var calculator = new StationOffsetCalculator();

            // Act
            StationOffsetResult result = calculator.Compute(polyline, userPoint);

            // Assert:
            // The first segment is 10 units. On the second segment, the projection occurs at (10,5) – an extra 5 units.
            // So station should be 15, and the offset is 2 (distance from (12,5) to (10,5)).
            Assert.That(result.Station, Is.EqualTo(15).Within(0.001));
            Assert.That(result.Offset, Is.EqualTo(2).Within(0.001));
        }
    }
}
