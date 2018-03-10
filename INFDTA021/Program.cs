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
            GetDictionary();

            Console.ReadKey();
        }

        private static void CalculateEuclidian(UserPreference one, UserPreference two)
        {
            //Calculate distance
            var distance = new SimilarityCalculator().Euclidian(one, two);
            Console.WriteLine("Distance calculated by using Euclidian = " + distance.ToString("N2"));
        }

        private static void CalculateManhattan(UserPreference one, UserPreference two)
        {
            //Calculate distance
            var distance = new SimilarityCalculator().Manhattan(one, two);
            Console.WriteLine("Distance calculated by using Manhattan = " + distance);
        }

        private static void CalculatePearson(UserPreference one, UserPreference two)
        {
            //Calculate distance
            var correlation = new SimilarityCalculator().Pearson(one, two);
            Console.WriteLine("Correlation calculated by using Pearson = " + correlation);
        }

        private static void CalculateCosine(UserPreference one, UserPreference two)
        {
            //Calculate cosine similarity
            var cosine = new SimilarityCalculator().Cosine(one, two);
            Console.WriteLine("Similarity calculated by using Cosine = " + cosine.ToString("N3"));
        }

        private static void FindNearestNeighbour(Dictionary<int, UserPreference> userPreferences, UserPreference target)
        {
            List<UserPreference> nearestNeighbourEuclidian = new List<UserPreference>();
            List<UserPreference> nearestNeighbourPearson = new List<UserPreference>();
            List<UserPreference> nearestNeighbourCosine = new List<UserPreference>();

            UserPreference previous = null;
            foreach (KeyValuePair<int, UserPreference> keyPair in userPreferences)
            {
                int user = keyPair.Key;
                UserPreference preference = keyPair.Value;


            }
        }

        private static void GetDictionary()
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

            //Run exercises
            SlideExercises();
            PearsonBetweenUser3And4(userPreferences);
        }

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

            CalculateEuclidian(amy, x);
            CalculateManhattan(amy, x);
            CalculatePearson(clara, robert);
            CalculateCosine(clara, robert);
        }

        private static void PearsonBetweenUser3And4(Dictionary<int, UserPreference>  userPreferences)
        {
            CalculatePearson(userPreferences.Values.FirstOrDefault(s => s.UserId == 3), 
                userPreferences.Values.FirstOrDefault(s => s.UserId == 4));
        }

    }
}
