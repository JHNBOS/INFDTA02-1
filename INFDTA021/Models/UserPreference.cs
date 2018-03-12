using System;
using System.Collections.Generic;

namespace Assignment1.Models
{
    public class UserPreference
    {
        public int UserId { get; set; }
        public Dictionary<int, double> Ratings { get; set; }

        public UserPreference(int userId, Dictionary<int, double> ratings)
        {
            this.UserId = userId;
            this.Ratings = ratings;
        }

        public void AddRating(int itemId, double rating)
        {
            Ratings.Add(itemId, rating);
        }

        public void RemoveRating(int itemId)
        {
            Ratings.Remove(itemId);
        }

        public override string ToString()
        {
            var rated = String.Format("\nUser {0} rated the following item(s):\n\n", this.UserId);
            foreach (var rating in this.Ratings)
            {
                rated += String.Format("\tItem {0} has been rated with a {1}\n", rating.Key, rating.Value.ToString("N1"));
            }
            rated += "\n-------------------------------------------------";

            return rated;
        }
    }
}
