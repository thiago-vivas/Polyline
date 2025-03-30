using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThiagoAraujo.Models;

namespace ThiagoAraujo.Interfaces
{
    // Abstraction for calculating station and offset.
    public interface IStationOffsetCalculator
    {
        StationOffsetResult Compute(List<Point> polyline, Point userPoint);
    }
}
