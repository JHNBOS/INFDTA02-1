using System;
using System.Collections.Generic;
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
            }

            //Check if each article has a rating
            foreach (var rating in userTwo.Ratings)
            {
                int? itemId = rating.Key;
                double? givenRating = rating.Value;

                if (itemId != null && givenRating != null)
                    userTwoRating.Add((int)itemId, (double)givenRating);
            }

            //Calculate the Cosine similarity
            double cosine = 0;
            double sumXY = 0;
            double sumX = 0;
            double sumY = 0;

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
    }
}
