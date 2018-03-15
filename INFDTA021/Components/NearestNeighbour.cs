using System.Collections.Generic;
using System.Linq;
using Assignment1.Components.Similarities;
using Assignment1.Models;

namespace Assignment1.Components
{
    public class NearestNeighbour
    {
        private Dictionary<int, double> neighbours = new Dictionary<int, double>();

        public Dictionary<int, double> FindNearestNeighbour(Dictionary<int, Dictionary<int, double>> ratings,
                int targetUser, double threshold, int max, Similarity similarityType)
        {
            //Get target user from list of ratings
            var target = ratings.FirstOrDefault(q => q.Key == targetUser).Value;

            foreach (var user in ratings)
            {
                if (user.Key != targetUser)
                {
                    double similarity = 0;

                    //Convert ratings of both users to vectors
                    var vectors = new Helper().ConvertToVector(user.Value, target, similarityType);

                    //Calculate similarity based on similarity type
                    switch (similarityType)
                    {
                        case Similarity.Euclidian:
                            similarity = new Euclidian().Calculate(vectors.Item1, vectors.Item2);
                            break;
                        case Similarity.Pearson:
                            similarity = new Pearson().Calculate(vectors.Item1, vectors.Item2);
                            break;
                        case Similarity.Cosine:
                            similarity = new Cosine().Calculate(vectors.Item1, vectors.Item2);
                            break;
                        default:
                            break;
                    }

                    //Check if similarity is above threshold
                    if (similarity > threshold && HasRatedAdditionalItems(vectors.Item1, vectors.Item2))
                    {
                        if (neighbours.Count < max)
                        {
                            neighbours.Add(user.Key, similarity);
                        } else
                        {
                            var lowest = neighbours.OrderBy(o => o.Value).ElementAt(0);
                            if (similarity > lowest.Value)
                            {
                                neighbours.Remove(lowest.Key);
                                neighbours.Add(user.Key, similarity);
                            }
                        }
                    }
                }
            }

            return neighbours;
        }

        private bool HasRatedAdditionalItems(Vector userToCompare, Vector target)
        {
            bool hasRatedSameItems = userToCompare.GetPoints().Any(q => !target.GetPoints().Contains(q));
            return hasRatedSameItems;
        }

    }
}
