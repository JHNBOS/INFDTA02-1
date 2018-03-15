using System;
using Assignment1.Components.Interfaces;
using Assignment1.Models;

namespace Assignment1.Components.Similarities
{
    public class Cosine : IStrategy
    {
        public double Calculate(Vector vectorOne, Vector vectorTwo)
        {
            double similarity = 0;
            double totalXY = 0;
            double totalX = 0;
            double totalY = 0;

            for (int i = 0; i < vectorOne.Size(); i++)
            {
                totalX += Math.Pow(vectorOne.GetPoints()[i], 2);
                totalY += Math.Pow(vectorTwo.GetPoints()[i], 2); ;
                totalXY += (vectorOne.GetPoints()[i] * vectorTwo.GetPoints()[i]);
            }

            similarity = totalXY / (Math.Sqrt(totalX) * Math.Sqrt(totalY));
            return similarity;
        }
    }
}
