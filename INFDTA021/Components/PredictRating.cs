using System.Collections.Generic;
using System.Linq;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class PredictRating
    {
        public double PredictRatingByNeighbours(Dictionary<int, UserPreference> userPreferences,
            UserPreference targetUser, int itemToRate, double threshold, int max, Similarity similarityType)
        {
            //Get nearest neighbours of target with their similarity
            var nearestNeighbours = new NearestNeighbour().FindNearestNeighbour(userPreferences, targetUser.UserId, 
                threshold, max, similarityType);

            double numerator = 0;
            double denominator = 0;

            foreach (KeyValuePair<int, UserPreference> userPreference in userPreferences
                .Where(q => q.Key != targetUser.UserId).ToDictionary(k => k.Key, v => v.Value))
            {

                foreach (KeyValuePair<int, double> keyValue in nearestNeighbours)
                {
                    int userId = keyValue.Key;
                    double similarity = keyValue.Value;

                    if (userId == userPreference.Key)
                    {
                        double givenRating = 0;
                        userPreference.Value.Ratings.TryGetValue(itemToRate, out givenRating);

                        numerator += (similarity * givenRating);
                        denominator += similarity;
                    }
                }
            }

            return (numerator / denominator);
        }
    }
}
