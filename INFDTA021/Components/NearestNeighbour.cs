using System.Collections.Generic;
using System.Linq;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class NearestNeighbour
    {
        public Dictionary<int, double> FindNearestNeighbour(Dictionary<int, UserPreference> userPreferences,
                int targetUser, double threshold, int max, Similarity similarityType)
        {
            //Get target user
            var target = userPreferences.FirstOrDefault(q => q.Key == targetUser).Value;

            //Skip target user
            var list = userPreferences.OrderBy(o => o.Key).Where(q => q.Key != targetUser)
                .ToDictionary(k => k.Key, v => v.Value);

            //Loop through each user in the list, and if similarity is above the threshold,
            //add user to list of neighbours
            var results = new Dictionary<int, double>();
            foreach (var neighbour in list)
            {
                int user = neighbour.Key;
                UserPreference preference = neighbour.Value;

                switch (similarityType)
                {
                    case Similarity.Euclidian:
                        var euclidian = 1 / (1 + new SimilarityCalculator().Euclidian(preference, target));
                        if (euclidian > threshold && HasRatedAdditionalItems(preference, target))
                        {
                            if (results.Count < max)
                            {
                                results.Add(user, euclidian);
                            } else
                            {
                                var lowest = results.OrderBy(o => o.Value).ElementAt(0);
                                if (euclidian > lowest.Value)
                                    results.Add(user, euclidian);
                            }
                        }

                        results = results.OrderBy(o => o.Value).Take(max)
                            .ToDictionary(k => k.Key, v => v.Value);

                        break;
                    case Similarity.Pearson:
                        var pearson = new SimilarityCalculator().Pearson(preference, target);
                        if (pearson > threshold && HasRatedAdditionalItems(preference, target))
                        {
                            if (results.Count < max)
                            {
                                results.Add(user, pearson);
                            } else
                            {
                                var lowest = results.OrderBy(o => o.Value).ElementAt(0);
                                if (pearson > lowest.Value)
                                    results.Add(user, pearson);
                            }
                        }

                        results = results.OrderByDescending(o => o.Value).Take(max)
                            .ToDictionary(k => k.Key, v => v.Value);

                        break;
                    case Similarity.Cosine:
                        var cosine = new SimilarityCalculator().Cosine(preference, target);
                        if (cosine > threshold && HasRatedAdditionalItems(preference, target))
                        {
                            if (results.Count < max)
                            {
                                results.Add(user, cosine);
                            } else
                            {
                                var lowest = results.OrderBy(o => o.Value).ElementAt(0);
                                if (cosine > lowest.Value)
                                    results.Add(user, cosine);
                            }
                        }

                        results = results.OrderByDescending(o => o.Value).Take(max)
                            .ToDictionary(k => k.Key, v => v.Value);

                        break;
                }
            }

            return results;
        }

        private bool HasRatedAdditionalItems(UserPreference userToCompare, UserPreference target)
        {
            bool hasRatedSameItems = userToCompare.Ratings.Keys.Any(q => !target.Ratings.Keys.Contains(q));
            return hasRatedSameItems;
        }

    }
}
