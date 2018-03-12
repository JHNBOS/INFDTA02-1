using System.Collections.Generic;
using System.Linq;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class PredictRatingCalculator
    {
        public double PredictRatingByNeighbours(Dictionary<int, UserPreference> userPreferences, 
            int targetUser, int itemToRate, double threshold, int max)
        {
            NearestNeighbourCalculator nearestNeighbourCalculator = new NearestNeighbourCalculator();
            Dictionary<int, double> pearsonList;

            //Get target user by user id
            var target = userPreferences.FirstOrDefault(q => q.Key == targetUser).Value;

            //Get nearest neighbours of target
            var nearestNeighbours = nearestNeighbourCalculator.FindNearestNeighbour(userPreferences, targetUser, 
                threshold, max);

            //Get pearson coefficient from nearest neighbours
            nearestNeighbours.TryGetValue(2, out pearsonList);

            double numerator = 0;
            double denominator = 0;

            foreach (KeyValuePair<int, UserPreference> userPreference in userPreferences
                .OrderBy(o => o.Key).Where(q => q.Key != targetUser)
                .ToDictionary(k => k.Key, v => v.Value))
            {
                foreach (KeyValuePair<int, double> keyValue in pearsonList.OrderByDescending(o => o.Value).Take(max))
                {
                    int userId = keyValue.Key;
                    double pearson = keyValue.Value;

                    if (userId == userPreference.Key)
                    {
                        double givenRating;
                        userPreference.Value.Ratings.TryGetValue(itemToRate, out givenRating);

                        numerator += (pearson * givenRating);
                        denominator += pearson;
                    }
                }
            }

            return (numerator / denominator);
        }
    }
}
