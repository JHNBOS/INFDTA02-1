using System;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class SimilarityCalculator
    {
        public double Euclidian(Vector vectorOne, Vector vectorTwo)
        {
            double similarity = 0;

            for (int i = 0; i < vectorOne.Size(); i++)
            {
                similarity += Math.Pow((vectorOne.GetPoints()[i] - vectorTwo.GetPoints()[i]), 2);
            }
            similarity = Math.Sqrt(similarity);

            return 1 / (1 + similarity);
        }

        public double Manhattan(Vector vectorOne, Vector vectorTwo)
        {
            double similarity = 0;

            for (int i = 0; i < vectorOne.Size(); i++)
            {
                similarity += (vectorOne.GetPoints()[i] - vectorTwo.GetPoints()[i]);
            }

            return similarity;
        }

        public double Pearson(Vector vectorOne, Vector vectorTwo)
        {
            double similarity = 0;
            double totalXY = 0;
            double totalX= 0;
            double totalY = 0;
            double totalPowerX = 0;
            double totalPowerY = 0;
            int total = 0;

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

        public double Cosine(Vector vectorOne, Vector vectorTwo)
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
