using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment1
{
    public class FileReader
    {
        public List<int> GetUsers(string filepath)
        {
            List<int> users = File.ReadAllLines(filepath)
                .Select(x => int.Parse(x.Split(',')[0]))
                .ToList();

            //Remove all duplicate values
            users = users.Distinct().ToList();

            //Sort list
            users.Sort();

            return users;
        }

        public List<string[]> GetRatings(string filepath)
        {
            List<string[]> ratings = File.ReadAllLines(filepath)
                .Select(x => x.Split(','))
                .ToList();
            return ratings;
        }

    }
}
