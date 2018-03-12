using System;
using System.Linq;
using System.Collections.Generic;
using Assignment1.Components;
using Assignment1.Models;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get dictionary
            var data = GetDictionary();

            //Run exercises
            //SlideExercises
            //AssignmentOnedotOne(data);
            //AssignmentOnedotTwo(data);
            //AssignmentOnedotThree(data);
            AssignmentOnedotFour(data);

            Console.ReadKey();
        }

        #region Calculate Methods

        private static double CalculateEuclidian(UserPreference one, UserPreference two)
        {
            var distance = new SimilarityCalculator().Euclidian(one, two);
            return distance;
        }

        private static double CalculateManhattan(UserPreference one, UserPreference two)
        {
            var distance = new SimilarityCalculator().Manhattan(one, two);
            return distance;
        }

        private static double CalculatePearson(UserPreference one, UserPreference two)
        {
            var correlation = new SimilarityCalculator().Pearson(one, two);
            return correlation;
        }

        private static double CalculateCosine(UserPreference one, UserPreference two)
        {
            var cosine = new SimilarityCalculator().Cosine(one, two);
            return cosine;
        }

        private static void FindNearestNeighbour(Dictionary<int, UserPreference> userPreferences, int targetUser,
            double threshold, int max)
        {
            Dictionary<int, double> nearestNeighbourEuclidian = new Dictionary<int, double>();
            Dictionary<int, double> nearestNeighbourPearson = new Dictionary<int, double>();
            Dictionary<int, double> nearestNeighbourCosine = new Dictionary<int, double>();

            //Get nearest neighbours
            var nearestNeighbours = new NearestNeighbourCalculator().FindNearestNeighbour(userPreferences, targetUser,
                threshold, max);
            nearestNeighbours.TryGetValue(1, out nearestNeighbourEuclidian);
            nearestNeighbours.TryGetValue(2, out nearestNeighbourPearson);
            nearestNeighbours.TryGetValue(3, out nearestNeighbourCosine);

            Console.WriteLine("\nNearest neighbours using Euclidian:");
            Console.WriteLine("------------------------------------------");
            foreach (KeyValuePair<int, double> item in nearestNeighbourEuclidian.OrderBy(o => o.Value).Take(max))
            {
                Console.WriteLine("User " + item.Key + " with a distance of " + item.Value);
            }

            Console.WriteLine("\nNearest neighbours using Pearson:");
            Console.WriteLine("------------------------------------------");
            foreach (KeyValuePair<int, double> item in nearestNeighbourPearson.OrderByDescending(o => o.Value).Take(max))
            {
                Console.WriteLine("User " + item.Key + " with a distance of " + item.Value);
            }

            Console.WriteLine("\nNearest neighbours using Cosine:");
            Console.WriteLine("------------------------------------------");
            foreach (KeyValuePair<int, double> item in nearestNeighbourCosine.OrderByDescending(o => o.Value).Take(max))
            {
                Console.WriteLine("User " + item.Key + " with a correlation of " + item.Value);
            }
        }

        private static void PredictRating(Dictionary<int, UserPreference> userPreferences,
            int targetUser, List<int> itemsToRate, double threshold, int max)
        {
            Console.WriteLine("\nPrediction(s) for user " + targetUser + ":");
            Console.WriteLine("------------------------------------------");

            foreach (var item in itemsToRate)
            {
                var prediction = new PredictRatingCalculator().PredictRatingByNeighbours(userPreferences,
                targetUser, item, threshold, max);
                Console.WriteLine("Item " + item + " with a rating of " + prediction);
            }
        }

        #endregion

        #region GetData

        private static Dictionary<int, UserPreference> GetDictionary()
        {
            //Create dictionary to hold user preferences
            Dictionary<int, UserPreference> userPreferences = new Dictionary<int, UserPreference>();

            //Create new instance of FileReader
            FileReader fileReader = new FileReader();

            Console.WriteLine("Please enter the path to the dataset to use:");
            string filepath = Console.ReadLine();

            foreach (var userId in fileReader.GetUsers(filepath))
            {
                //Create dictionary to hold user ratings
                Dictionary<int, double> ratings = new Dictionary<int, double>();

                //Create new UserPreference for current user id
                UserPreference userPreference = new UserPreference(userId, ratings);

                //Add preference to dictionary
                userPreferences.Add(userId, userPreference);

                //Get all lines of given file
                var ratingData = fileReader.GetRatings(filepath);

                //Create temporary array for all occurances containing the user id
                List<string[]> occurances = new List<string[]>();

                //Compare current user id to user id in ratingData,
                //if it equals, add the row to the list
                foreach (var row in ratingData)
                {
                    var userIdFromData = int.Parse(row[0]);
                    if (userId == userIdFromData)
                    {
                        occurances.Add(row);
                    }
                }

                //Loop through all occurances and add rating to current user id
                foreach (var occurance in occurances)
                {
                    var articleId = int.Parse(occurance[1]);
                    var rating = (double.Parse(occurance[2]) / 10);

                    userPreference.AddRating(articleId, rating);
                }
            }

            return userPreferences;
        }

        #endregion

        #region Assignments

        private static void SlideExercises()
        {
            var listX = new Dictionary<int, double>();
            listX.Add(1, 4);
            listX.Add(2, 2);
            UserPreference x = new UserPreference(1, listX);

            var listAmy = new Dictionary<int, double>();
            listAmy.Add(1, 5);
            listAmy.Add(2, 5);
            UserPreference amy = new UserPreference(2, listAmy);

            var listClara = new Dictionary<int, double>();
            listClara.Add(1, 4.75);
            listClara.Add(2, 4.5);
            listClara.Add(3, 5);
            listClara.Add(4, 4.25);
            listClara.Add(5, 4);
            UserPreference clara = new UserPreference(1, listClara);

            var listRobert = new Dictionary<int, double>();
            listRobert.Add(1, 4);
            listRobert.Add(2, 3);
            listRobert.Add(3, 5);
            listRobert.Add(4, 2);
            listRobert.Add(5, 1);
            UserPreference robert = new UserPreference(2, listRobert);

            var listAmy2 = new Dictionary<int, double>();
            listAmy2.Add(1, 3.0);
            UserPreference amyTwo = new UserPreference(3, listAmy2);

            var listX2 = new Dictionary<int, double>();
            listX2.Add(1, 5.0);
            listX2.Add(2, 2.5);
            listX2.Add(3, 2.0);
            UserPreference xTwo = new UserPreference(4, listX2);

            var one = CalculateEuclidian(amy, x);
            var two = CalculateManhattan(amy, x);
            var three = CalculatePearson(clara, robert);
            var four = CalculateCosine(clara, robert);
            var five = CalculateCosine(amyTwo, xTwo);

            Console.WriteLine("Euclidian between Amy and X = " + one);
            Console.WriteLine("Manhattann between Amy and X = " + two);
            Console.WriteLine("Pearson between Clara and Robert = " + three);
            Console.WriteLine("Cosine between Clara and Robert = " + four);
            Console.WriteLine("Cosine between Amy and X (incomplete) = " + five);
        }

        private static void AssignmentOnedotOne(Dictionary<int, UserPreference> userPreferences)
        {
            CalculatePearson(userPreferences.Values.FirstOrDefault(s => s.UserId == 3),
                userPreferences.Values.FirstOrDefault(s => s.UserId == 4));
        }

        private static void AssignmentOnedotTwo(Dictionary<int, UserPreference> userPreferences)
        {
            FindNearestNeighbour(userPreferences, 7, 0.35, 3);
        }

        private static void AssignmentOnedotThree(Dictionary<int, UserPreference> userPreferences)
        {
            List<int> items = new List<int>();
            items.Add(101);
            items.Add(103);
            items.Add(106);

            PredictRating(userPreferences, 7, items, 0.35, 3);
        }

        private static void AssignmentOnedotFour(Dictionary<int, UserPreference> userPreferences)
        {
            List<int> items = new List<int>();
            items.Add(101);

            PredictRating(userPreferences, 4, items, 0.35, 3);
        }

        #endregion

    }
}
