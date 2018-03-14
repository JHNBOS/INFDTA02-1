using System.Collections.Generic;

namespace Assignment1.Models
{
    public class Vector
    {
        private List<double> Points { get; set; }

        public Vector()
        {
            this.Points = new List<double>();
        }

        public Vector(List<double> points)
        {
            this.Points = points;
        }

        public void AddPoint(double point)
        {
            Points.Add(point);
        }

        public List<double> GetPoints()
        {
            return this.Points;
        }

        public int Size()
        {
            return this.Points.Count;
        }
    }
}
