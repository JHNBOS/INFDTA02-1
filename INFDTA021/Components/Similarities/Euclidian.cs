using System;
using Assignment1.Components.Interfaces;
using Assignment1.Models;

namespace Assignment1.Components.Similarities
{
    public class Euclidian : IStrategy
    {
        public double Calculate(Vector vectorOne, Vector vectorTwo)
        {
            double similarity = 0;

            for (int i = 0; i < vectorOne.Size(); i++)
            {
                similarity += Math.Pow((vectorOne.GetPoints()[i] - vectorTwo.GetPoints()[i]), 2);
            }
            similarity = Math.Sqrt(similarity);

            return 1 / (1 + similarity);
        }
    }
}
