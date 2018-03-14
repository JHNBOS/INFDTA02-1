using System;
using System.Collections.Generic;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class Helper
    {
        public Tuple<Vector, Vector> ConvertToVector(Dictionary<int, double> userOne, Dictionary<int, double> userTwo,
            Similarity similarityType)
        {
            Vector vectorOne = new Vector();
            Vector vectorTwo = new Vector();

            // Loop through list of items rated
            foreach (var item in userOne.Keys)
            {
                if (similarityType == Similarity.Cosine)
                {
                    // If the item is rated by both, add to vector, else add 0 to second vector
                    if (userTwo.ContainsKey(item))
                    {
                        vectorOne.AddPoint(userOne[item]);
                        vectorTwo.AddPoint(userTwo[item]);
                    } else
                    {
                        vectorOne.AddPoint(userOne[item]);
                        vectorTwo.AddPoint(0);
                    }
                } else
                {
                    // If the item is rated by both, add to vector
                    if (userTwo.ContainsKey(item))
                    {
                        vectorOne.AddPoint(userOne[item]);
                        vectorTwo.AddPoint(userTwo[item]);
                    }
                }
            }

            return new Tuple<Vector, Vector>(vectorOne, vectorTwo);
        }
    }
}
