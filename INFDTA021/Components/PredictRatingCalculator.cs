using System;
using System.Collections.Generic;
using System.Linq;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class PredictRatingCalculator
    {
        public Dictionary<int, double> PredictRatingByNeighbours(Dictionary<int, UserPreference> userPreferences, 
            UserPreference target, List<int> itemsToRate, double threshold, int max)
        {
            SimilarityCalculator similarityCalculator = new SimilarityCalculator();
            NearestNeighbourCalculator nearestNeighbourCalculator = new NearestNeighbourCalculator();

            //Get nearest neighbours of target
            var nearestNeighbours = nearestNeighbourCalculator.FindNearestNeighbour(userPreferences, target, threshold, max);

            Dictionary<int, double> pearsonList;
            nearestNeighbours.TryGetValue(2, out pearsonList);

            //Create dictionary with predictions for each item to rate
            //Tuple will contain the numerator and denominator of each prediction
            Dictionary<int, Tuple<double, double>> calculations = new Dictionary<int, Tuple<double, double>>();
            foreach (var item in itemsToRate)
            {
                calculations.Add(item, new Tuple<double, double>(0, 0));
            }

            foreach (KeyValuePair<int, UserPreference> userPreference in userPreferences)
            {
                foreach (KeyValuePair<int, double> keyValue in pearsonList)
                {
                    int userId = keyValue.Key;
                    double pearson = keyValue.Value;

                    if (userId == userPreference.Key)
                    {
                        int count = 0;

                        while (count < itemsToRate.Count)
                        {
                            double givenRating;
                            userPreference.Value.Ratings.TryGetValue(itemsToRate[count], out givenRating);

                            Tuple<double, double> oldSum;
                            calculations.TryGetValue(itemsToRate[count], out oldSum);

                            //Numerator
                            var oldNumerator = oldSum.Item1;
                            oldNumerator += (pearson * givenRating);
                            //Denominator
                            var oldDenominator = oldSum.Item2;
                            oldDenominator += pearson;

                            Tuple<double, double> newSum = new Tuple<double, double>(oldNumerator, oldDenominator);

                            //Remove old value
                            calculations.Remove(itemsToRate[count]);

                            //Add new value
                            calculations.Add(itemsToRate[count], newSum);

                            count++;
                        }
                    }
                }
            }

            Dictionary<int, double> predictions = new Dictionary<int, double>();
            foreach (KeyValuePair<int, Tuple<double, double>> calculation in calculations)
            {
                var numerator = calculation.Value.Item1;
                var denominator = calculation.Value.Item2;

                var calc = (numerator / denominator);
                predictions.Add(calculation.Key, calc);
            }

            return predictions;
        }
    }
}
