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
            Dictionary<int, double> neighbours, int targetUser, double threshold, 
            int maxNeighhbours, int maxResults, int? minimumRatings, Similarity similarityType)
        {
            var predictions = new Dictionary<int, double>();
            var items = new List<int>();

            foreach (var user in ratings.Values)
            {
                foreach (var item in user)
                {
                    if (!items.Contains(item.Key))
                    {
                        items.Add(item.Key);
                    }
                }
            }

            //Loop through all items and predict the rating
            foreach (var item in items)
            {
                var itemCount = 0;
                if (itemCount < minimumRatings)
                {
                    foreach (var neighbour in neighbours)
                    {
                        if (ratings[neighbour.Key].ContainsKey(item))
                        {
                            itemCount++;
                        }
                    }
                }
                if (itemCount >= minimumRatings || minimumRatings == null)
                {
                    var prediction = PredictRatingByNeighbours(ratings, targetUser, item, threshold, maxNeighhbours,
                    similarityType);
                    predictions.Add(item, prediction);
                }
            }

            return predictions.OrderByDescending(o => o.Value).Take(maxResults).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
