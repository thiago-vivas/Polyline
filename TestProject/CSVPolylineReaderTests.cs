using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using FugroProgrammingExercise;
using ThiagoAraujo.Polyline;
using ThiagoAraujo.Models;

namespace FugroProgrammingExercise.Tests
{
    [TestFixture]
    public class CSVPolylineReaderTests
    {
        private string tempFilePath;

        [SetUp]
        public void Setup()
        {
            // Create a temporary file for testing.
            tempFilePath = Path.GetTempFileName();
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up the temporary file after tests.
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }

        [Test]
        public void ReadPolyline_ValidFile_ReturnsCorrectPoints()
        {
            // Arrange: Create file content with valid points.
            string content = "150,200\r\n100,45\r\n20,-40";
            File.WriteAllText(tempFilePath, content);

            var reader = new CSVPolylineReader();

            // Act
            List<Point> points = reader.ReadPolyline(tempFilePath);

            // Assert using constraint model.
            Assert.That(points.Count, Is.EqualTo(3));
            Assert.That(points[0].X, Is.EqualTo(150));
            Assert.That(points[0].Y, Is.EqualTo(200));
            Assert.That(points[1].X, Is.EqualTo(100));
            Assert.That(points[1].Y, Is.EqualTo(45));
            Assert.That(points[2].X, Is.EqualTo(20));
            Assert.That(points[2].Y, Is.EqualTo(-40));
        }

        [Test]
        public void ReadPolyline_InvalidLine_IgnoresInvalidLine()
        {
            // Arrange: Include an invalid line in the file.
            string content = "150,200\r\ninvalid,line\r\n20,-40";
            File.WriteAllText(tempFilePath, content);

            var reader = new CSVPolylineReader();

            // Act
            List<Point> points = reader.ReadPolyline(tempFilePath);

            // Assert: Only valid lines are added.
            Assert.That(points.Count, Is.EqualTo(2));
            Assert.That(points[0].X, Is.EqualTo(150));
            Assert.That(points[0].Y, Is.EqualTo(200));
            Assert.That(points[1].X, Is.EqualTo(20));
            Assert.That(points[1].Y, Is.EqualTo(-40));
        }
    }

}
