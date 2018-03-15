using Assignment1.Components.Interfaces;
using Assignment1.Models;

namespace Assignment1.Components.Similarities
{
    public class Manhattan : IStrategy
    {
        public double Calculate(Vector vectorOne, Vector vectorTwo)
        {
            double similarity = 0;

            for (int i = 0; i < vectorOne.Size(); i++)
            {
                similarity += (vectorOne.GetPoints()[i] - vectorTwo.GetPoints()[i]);
            }

            return similarity;
        }
    }
}
