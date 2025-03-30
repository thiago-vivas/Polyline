using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThiagoAraujo.Models;

namespace ThiagoAraujo.Interfaces
{
    // Abstraction for reading a polyline.
    public interface IPolylineReader
    {
        List<Point> ReadPolyline(string filePath);
    }
}
