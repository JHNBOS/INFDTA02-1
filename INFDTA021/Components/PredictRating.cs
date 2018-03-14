using System.Collections.Generic;
using System.Linq;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class PredictRating
    {
        public double PredictRatingByNeighbours(Dictionary<int, Dictionary<int, double>> ratings,
            int targetUser, int itemToRate, double threshold, int max, Similarity similarityType)
        {
            //Get nearest neighbours of target with their similarity
            var nearestNeighbours = new NearestNeighbour().FindNearestNeighbour(ratings, targetUser, 
                threshold, max, similarityType);

            double numerator = 0;
            double denominator = 0;

            foreach (var neighbour in nearestNeighbours)
            {
                if (ratings[neighbour.Key].ContainsKey(itemToRate))
                {
                    //Get pearson coefficient from current neighbour
                    //Get given rating from current neighbour
                    double similarity = neighbour.Value;
                    double givenRating = ratings[neighbour.Key][itemToRate];

                    numerator += (similarity * givenRating);
                    denominator += similarity;
                }
            }

            return (numerator / denominator);
        }

        public Dictionary<int, double> PredictTopRatings(Dictionary<int, Dictionary<int, double>> ratings,
            int targetUser, double threshold, int maxNeighhbours, int maxResults,
            Similarity similarityType)
        {
            var predictions = new Dictionary<int, double>();
            var items = new List<int>();

            foreach (var user in ratings)
            {
                foreach (var item in user.Value)
                {
                    if (items.Contains(item.Key) == false)
                    {
                        items.Add(item.Key);
                    }
                }
            }

            //Loop through all items and predict the rating
            foreach (var item in items)
            {
                var prediction = PredictRatingByNeighbours(ratings, targetUser, item, threshold, maxNeighhbours, 
                    similarityType);
                predictions.Add(item, prediction);
            }

            return predictions.OrderByDescending(o => o.Value).Take(maxResults).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
