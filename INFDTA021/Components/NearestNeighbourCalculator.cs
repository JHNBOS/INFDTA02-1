using System.Collections.Generic;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class NearestNeighbourCalculator
    {
        public Dictionary<int, Dictionary<int, double>> FindNearestNeighbour(Dictionary<int, UserPreference> userPreferences,
                UserPreference target, double threshold, int max)
        {
            Dictionary<int, double> nearestNeighbourEuclidian = new Dictionary<int, double>();
            Dictionary<int, double> nearestNeighbourPearson = new Dictionary<int, double>();
            Dictionary<int, double> nearestNeighbourCosine = new Dictionary<int, double>();

            SimilarityCalculator similarityCalculator = new SimilarityCalculator();

            //Remove current user from dictionary
            userPreferences.Remove(target.UserId);

            foreach (KeyValuePair<int, UserPreference> keyPair in userPreferences)
            {
                int user = keyPair.Key;
                UserPreference preference = keyPair.Value;

                var euclidian = 1 / (1 + similarityCalculator.Euclidian(preference, target));
                var pearson = similarityCalculator.Pearson(preference, target);
                var cosine = similarityCalculator.Cosine(preference, target);

                //Check for euclidian
                if (euclidian > threshold)
                    nearestNeighbourEuclidian.Add(user, euclidian);

                //Check for pearson
                if (pearson > threshold)
                    nearestNeighbourPearson.Add(user, pearson);

                //Check for cosine
                if (cosine > threshold)
                    nearestNeighbourCosine.Add(user, cosine);
            }

            var listToReturn = new Dictionary<int, Dictionary<int, double>>();
            listToReturn.Add(1, nearestNeighbourEuclidian);
            listToReturn.Add(2, nearestNeighbourPearson);
            listToReturn.Add(3, nearestNeighbourCosine);

            return listToReturn;
        }
    }
}
