using System;
using System.Collections.Generic;
using System.Linq;
using Assignment1.Components;
using Assignment1.Models;

namespace Assignment1
{
    class Program
    {
        static SimilarityCalculator similarityCalculator;

        static void Main(string[] args)
        {
            //Init objects
            similarityCalculator = new SimilarityCalculator();

            //Get dataset
            var data = GetData(',');

            //Run exercises
            //ExercisesLessonOne();
            //AssignmentOnedotOne(data);
            //AssignmentOnedotTwo(data, 7, 0.35, 3, Similarity.EuclidianAndPearsonAndCosine);
            //AssignmentOnedotThree(data, 7, Similarity.Pearson);
            //AssignmentOnedotFour(data, 4, Similarity.Pearson);
            //AssignmentOnedotFive(data, 7, Similarity.Pearson);
            //AssignmentOnedotSix(data, 7, Similarity.Pearson);

            data = GetData('\t');
            AssignmentOnedotSeven(data, 186, Similarity.Pearson, 0.35, 25, 8);

            Console.ReadKey();
        }

        private static Dictionary<int, Dictionary<int, double>> GetData(char delimiter)
        {
            Console.WriteLine("Please enter the path to the dataset:");

            var path = Console.ReadLine();
            var data = new FileReader().Parse(delimiter, path);

            return data;
        }

        #region Assignments

        private static void ExercisesLessonOne()
        {
            var listX = new List<double>();
            listX.Add(4);
            listX.Add(2);
            Vector x = new Vector(listX);

            var listAmy = new List<double>();
            listAmy.Add(5);
            listAmy.Add(5);
            Vector amy = new Vector(listAmy);

            var listClara = new List<double>();
            listClara.Add(4.75);
            listClara.Add(4.5);
            listClara.Add(5);
            listClara.Add(4.25);
            listClara.Add(4);
            Vector clara = new Vector(listClara);

            var listRobert = new List<double>();
            listRobert.Add(4);
            listRobert.Add(3);
            listRobert.Add(5);
            listRobert.Add(2);
            listRobert.Add(1);
            Vector robert = new Vector(listRobert);

            var listAmy2 = new List<double>();
            listAmy2.Add(3.0);
            Vector amyTwo = new Vector(listAmy2);

            var listX2 = new List<double>();
            listX2.Add(5.0);
            listX2.Add(2.5);
            listX2.Add(2.0);
            Vector xTwo = new Vector(listX2);

            var one = similarityCalculator.Euclidian(amy, x);
            var two = similarityCalculator.Manhattan(amy, x);
            var three = similarityCalculator.Pearson(clara, robert);
            var four = similarityCalculator.Cosine(clara, robert);
            var five = similarityCalculator.Cosine(amyTwo, xTwo);

            Console.WriteLine("\nEuclidian between Amy and X = " + one);
            Console.WriteLine("Manhattan between Amy and X = " + two);
            Console.WriteLine("Pearson between Clara and Robert = " + three);
            Console.WriteLine("Cosine between Clara and Robert = " + four);
            Console.WriteLine("Cosine between Amy and X (incomplete) = " + five);
        }

        private static void AssignmentOnedotOne(Dictionary<int, Dictionary<int, double>> ratings)
        {
            //Get ratings from user three and four
            var userThree = ratings.FirstOrDefault(s => s.Key == 3).Value;
            var userFour = ratings.FirstOrDefault(s => s.Key == 4).Value;

            //Convert ratings to vectors
            var vectors = new Helper().ConvertToVector(userThree, userFour, Similarity.Pearson);

            //Calculate pearson coefficient
            var result = similarityCalculator.Pearson(vectors.Item1, vectors.Item2);

            Console.WriteLine("\n\nAssignment 1.1");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(String.Format("Pearson coefficient between user {0} and user {1} is {2}", 3, 4, result));
        }

        private static void AssignmentOnedotTwo(Dictionary<int, Dictionary<int, double>> ratings, int user,
            double threshold, int max, Similarity similarityType)
        {
            Console.WriteLine("\n\nAssignment 1.2");
            Console.WriteLine("------------------------------------------");

            var nearestNeighbours = new Dictionary<int, double>();
            if (similarityType == Similarity.EuclidianAndPearsonAndCosine)
            {
                var similarities = new Similarity[3];
                similarities[0] = Similarity.Euclidian;
                similarities[1] = Similarity.Pearson;
                similarities[2] = Similarity.Cosine;

                foreach (var sim in similarities)
                {
                    nearestNeighbours = new NearestNeighbour().FindNearestNeighbour(ratings, user, threshold,
                        max, sim);

                    var loop = new Dictionary<int, double>();

                    switch (sim)
                    {
                        case Similarity.Euclidian:
                            loop = nearestNeighbours.OrderBy(o => o.Value).Take(max)
                                .ToDictionary(k => k.Key, v => v.Value);
                            Console.WriteLine("Nearest neighbours using Euclidian are:\n");
                            break;
                        case Similarity.Pearson:
                            loop = nearestNeighbours.OrderByDescending(o => o.Value).Take(max)
                                .ToDictionary(k => k.Key, v => v.Value);
                            Console.WriteLine("\nNearest neighbours using Pearson are:\n");
                            break;
                        case Similarity.Cosine:
                            loop = nearestNeighbours.OrderByDescending(o => o.Value).Take(max)
                                .ToDictionary(k => k.Key, v => v.Value);
                            Console.WriteLine("\nNearest neighbours using Cosine are:\n");
                            break;
                        default:
                            break;
                    }

                    foreach (KeyValuePair<int, double> item in loop)
                    {
                        Console.WriteLine("\tUser " + item.Key + " with a distance of " + item.Value);
                    }
                }
            } else
            {
                nearestNeighbours = new NearestNeighbour().FindNearestNeighbour(ratings, user, threshold,
                    max, similarityType);

                Console.WriteLine("Nearest neighbours are:");
                foreach (KeyValuePair<int, double> item in nearestNeighbours)
                {
                    Console.WriteLine("\tUser " + item.Key + " with a distance of " + item.Value);
                }
            }
        }

        private static void AssignmentOnedotThree(Dictionary<int, Dictionary<int, double>> ratings, int targetUser, Similarity similarityType)
        {
            Console.WriteLine("\n\nAssignment 1.3");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Rating predictions for user " + targetUser);

            List<int> items = new List<int>();
            items.Add(101);
            items.Add(103);
            items.Add(106);

            foreach (var item in items)
            {
                var prediction = new PredictRating().PredictRatingByNeighbours(ratings, targetUser, item, 0.35, 
                    3, similarityType);

                Console.WriteLine("\tUser " + targetUser + " will rate item " + item + " with a rating of " + prediction);
            }
        }

        private static void AssignmentOnedotFour(Dictionary<int, Dictionary<int, double>> ratings, int targetUser, Similarity similarityType)
        {
            Console.WriteLine("\n\nAssignment 1.4");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Rating predictions for user " + targetUser);

            List<int> items = new List<int>();
            items.Add(101);

            foreach (var item in items)
            {
                var prediction = new PredictRating().PredictRatingByNeighbours(ratings, targetUser, item, 0.35,
                    3, similarityType);

                Console.WriteLine("\tUser " + targetUser + " will rate item " + item + " with a rating of " + prediction);
            }
        }

        private static void AssignmentOnedotFive(Dictionary<int, Dictionary<int, double>> ratings, int targetUser, Similarity similarityType)
        {
            Console.WriteLine("\n\nAssignment 1.5");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Rating predictions for user " + targetUser + " with item 106 rated with a 2.8");

            List<int> items = new List<int>();
            items.Add(101);
            items.Add(103);

            //Add rating for item
            ratings[7][106] = 2.8;

            foreach (var item in items)
            {
                var prediction = new PredictRating().PredictRatingByNeighbours(ratings, targetUser, item, 0.35,
                    3, similarityType);

                Console.WriteLine("\tUser " + targetUser + " will rate item " + item + " with a rating of " + prediction);
            }
        }

        private static void AssignmentOnedotSix(Dictionary<int, Dictionary<int, double>> ratings, int targetUser, Similarity similarityType)
        {
            Console.WriteLine("\n\nAssignment 1.6");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Rating predictions for user " + targetUser + " with item 106 rated with a 5.0");

            List<int> items = new List<int>();
            items.Add(101);
            items.Add(103);

            //Add rating for item
            ratings[7][106] = 5.0;

            foreach (var item in items)
            {
                var prediction = new PredictRating().PredictRatingByNeighbours(ratings, targetUser, item, 0.35,
                    3, similarityType);

                Console.WriteLine("\tUser " + targetUser + " will rate item " + item + " with a rating of " + prediction);
            }
        }

        private static void AssignmentOnedotSeven(Dictionary<int, Dictionary<int, double>> ratings, int targetUser, Similarity similarityType,
            double threshold, int maxNeighhbours, int maxResults)
        {
            Console.WriteLine("\n\nAssignment 1.7");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Rating predictions for user " + targetUser + " with item 106 rated with a 5.0");

            var predictions = new PredictRating().PredictTopRatings(ratings, targetUser,
                0.35, 25, 8, similarityType);

            foreach (var prediction in predictions)
            {
                Console.WriteLine("\tUser " + targetUser + " will rate item " + prediction.Key + " with a rating of " + prediction.Value);
            }
        }

        #endregion

    }
}
