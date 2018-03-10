using System;
using System.Collections.Generic;
using System.Linq;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class SimilarityCalculator
    {
        public double Euclidian(UserPreference userOne, UserPreference userTwo)
        {
            Dictionary<int, double> userOneRating = new Dictionary<int, double>();
            Dictionary<int, double> userTwoRating = new Dictionary<int, double>();

            //Check if each article has a rating
            foreach (var rating in userOne.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userOneRating.Add((int)itemId, (double)givenRating);
            }

            //Check if each article has a rating
            foreach (var rating in userTwo.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userTwoRating.Add((int)itemId, (double)givenRating);
            }

            //Calculate the Euclidian distance for each rating
            double distance = 0;
            foreach (var keyValueOne in userOne.Ratings)
                {
                    foreach (var keyValueTwo in userTwo.Ratings)
                    {
                        if (keyValueOne.Key == keyValueTwo.Key)
                        {
                            double ratingUserOne;
                            userOneRating.TryGetValue(keyValueOne.Key, out ratingUserOne);

                            double ratingUserTwo;
                            userTwoRating.TryGetValue(keyValueTwo.Key, out ratingUserTwo);

                            var sum = Math.Pow((ratingUserOne - ratingUserTwo), 2);
                            distance += sum;
                        }
                    }
                }

            return Math.Sqrt(distance);
        }

        public double Manhattan(UserPreference userOne, UserPreference userTwo)
        {
            Dictionary<int, double> userOneRating = new Dictionary<int, double>();
            Dictionary<int, double> userTwoRating = new Dictionary<int, double>();

            //Check if each article has a rating
            foreach (var rating in userOne.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userOneRating.Add((int)itemId, (double)givenRating);
            }

            //Check if each article has a rating
            foreach (var rating in userTwo.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userTwoRating.Add((int)itemId, (double)givenRating);
            }

            //Calculate the Euclidian distance for each rating
            double distance = 0;
            foreach (var keyValueOne in userOne.Ratings)
            {
                foreach (var keyValueTwo in userTwo.Ratings)
                {
                    if (keyValueOne.Key == keyValueTwo.Key)
                    {
                        double ratingUserOne;
                        userOneRating.TryGetValue(keyValueOne.Key, out ratingUserOne);

                        double ratingUserTwo;
                        userTwoRating.TryGetValue(keyValueTwo.Key, out ratingUserTwo);

                        var sum = (ratingUserOne - ratingUserTwo);
                        distance += sum;
                    }
                }
            }

            return distance;
        }

        public double Pearson(UserPreference userOne, UserPreference userTwo)
        {
            Dictionary<int, double?> userOneRating = new Dictionary<int, double?>();
            Dictionary<int, double?> userTwoRating = new Dictionary<int, double?>();

            //Check if each article has a rating
            foreach (var rating in userOne.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userOneRating.Add((int)itemId, (double)givenRating);
            }

            //Check if each article has a rating
            foreach (var rating in userTwo.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userTwoRating.Add((int)itemId, (double)givenRating);
            }

            //Calculate the Pearson correlation
            double coefficient = 0;
            double sumXY = 0;
            double sumX = 0;
            double sumPowerX = 0;
            double sumPowerY = 0;
            double sumY = 0;
            int total = 0;

            foreach (var keyValueOne in userOne.Ratings)
            {
                foreach (var keyValueTwo in userTwo.Ratings)
                {
                    if (keyValueOne.Key == keyValueTwo.Key)
                    {
                        double? ratingUserOne;
                        userOneRating.TryGetValue(keyValueOne.Key, out ratingUserOne);

                        double? ratingUserTwo;
                        userTwoRating.TryGetValue(keyValueTwo.Key, out ratingUserTwo);

                        sumX += ratingUserOne.Value;
                        sumY += ratingUserTwo.Value;
                        sumXY += (ratingUserOne.Value * ratingUserTwo.Value);

                        sumPowerX += Math.Pow(ratingUserOne.Value, 2);
                        sumPowerY += Math.Pow(ratingUserTwo.Value, 2);

                        total++;
                    }
                }
            }

            var topPart = (sumXY) - ((sumX * sumY) / total);
            var bottomPart = Math.Sqrt((sumPowerX - (Math.Pow(sumX, 2) / total)) * (sumPowerY - (Math.Pow(sumY, 2) / total)));
            coefficient = topPart / bottomPart;

            return coefficient;
        }

        public double Cosine(UserPreference userOne, UserPreference userTwo)
        {
            Dictionary<int, double> userOneRating = new Dictionary<int, double>();
            Dictionary<int, double> userTwoRating = new Dictionary<int, double>();

            //Check if each article has a rating
            foreach (var rating in userOne.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userOneRating.Add((int)itemId, (double)givenRating);
                else
                    userOneRating.Add((int)itemId, 0);
            }

            //Check if each article has a rating
            foreach (var rating in userTwo.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userTwoRating.Add((int)itemId, (double)givenRating);
                else
                    userTwoRating.Add((int)itemId, 0);
            }

            var newLists = AddMissingItems(userOneRating, userTwoRating);
            newLists.TryGetValue(1, out userOneRating);
            newLists.TryGetValue(2, out userTwoRating);

            //Calculate the Cosine similarity
            double cosine = 0;
            double sumXY = 0;
            double sumX = 0;
            double sumY = 0;

            foreach (var keyValueOne in userOneRating)
            {
                foreach (var keyValueTwo in userTwoRating)
                {
                    if (keyValueOne.Key == keyValueTwo.Key)
                    {
                        double ratingUserOne;
                        userOneRating.TryGetValue(keyValueOne.Key, out ratingUserOne);

                        double ratingUserTwo;
                        userTwoRating.TryGetValue(keyValueTwo.Key, out ratingUserTwo);

                        sumX += Math.Pow(ratingUserOne, 2);
                        sumY += Math.Pow(ratingUserTwo, 2); ;
                        sumXY += (ratingUserOne * ratingUserTwo);
                    }
                }
            }

            var topPart = sumXY;
            var sqrtX = Math.Sqrt(sumX);
            var sqrtY = Math.Sqrt(sumY);
            var bottomPart = (sqrtX) * (sqrtY);

            cosine = topPart / bottomPart;

            return cosine;
        }

        public Dictionary<int, Dictionary<int, double>> FindNearestNeighbour(Dictionary<int, UserPreference> userPreferences, 
            UserPreference target, double threshold, int max)
        {
            Dictionary<int, double> nearestNeighbourEuclidian = new Dictionary<int, double>();
            Dictionary<int, double> nearestNeighbourPearson = new Dictionary<int, double>();
            Dictionary<int, double> nearestNeighbourCosine = new Dictionary<int, double>();

            //Remove current user from dictionary
            userPreferences.Remove(target.UserId);

            foreach (KeyValuePair<int, UserPreference> keyPair in userPreferences)
            {
                int user = keyPair.Key;
                UserPreference preference = keyPair.Value;

                var euclidian = 1 / (1 + Euclidian(preference, target));
                var pearson = Pearson(preference, target);
                var cosine = Cosine(preference, target);

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

        private Dictionary<int, Dictionary<int, double>> AddMissingItems(Dictionary<int, double> userOneRatings, Dictionary<int, double> userTwoRatings)
        {
            int count = 0;
            while (userOneRatings.Count != userTwoRatings.Count)
            {
                if (userTwoRatings.Count < userOneRatings.Count)
                {
                    if (userTwoRatings.Keys.Contains(userOneRatings.ElementAt(count).Key) == false)
                    {
                        userTwoRatings.Add(userOneRatings.ElementAt(count).Key, 0);
                    }
                } else
                {
                    if (userOneRatings.Keys.Contains(userTwoRatings.ElementAt(count).Key) == false)
                    {
                        userOneRatings.Add(userTwoRatings.ElementAt(count).Key, 0);
                    }
                }

                count++;
            }

            var listToReturn = new Dictionary<int, Dictionary<int, double>>();
            listToReturn.Add(1, userOneRatings);
            listToReturn.Add(2, userTwoRatings);

            return listToReturn;
        }

    }
}
