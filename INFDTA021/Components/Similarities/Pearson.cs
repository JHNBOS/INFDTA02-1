using System;
using Assignment1.Components.Interfaces;
using Assignment1.Models;

namespace Assignment1.Components.Similarities
{
    public class Pearson : IStrategy
    {
        public double Calculate(Vector vectorOne, Vector vectorTwo)
        {
            int total = 0;
            double similarity = 0, totalXY = 0, totalX = 0, totalY = 0,
            totalPowerX = 0, totalPowerY = 0;

            for (int i = 0; i < vectorOne.Size(); i++)
            {
                totalX += vectorOne.GetPoints()[i];
                totalY += vectorTwo.GetPoints()[i];
                totalXY += (vectorOne.GetPoints()[i] * vectorTwo.GetPoints()[i]);

                totalPowerX += Math.Pow(vectorOne.GetPoints()[i], 2);
                totalPowerY += Math.Pow(vectorTwo.GetPoints()[i], 2);

                total++;
            }

            var numerator = totalXY - (totalX * totalY / total);
            var denominator = Math.Sqrt(totalPowerX - (Math.Pow(totalX, 2) / total)) * Math.Sqrt(totalPowerY - (Math.Pow(totalY, 2) / total));
            similarity = numerator / denominator;

            return similarity;
        }
    }
}
