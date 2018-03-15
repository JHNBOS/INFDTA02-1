using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Assignment1
{
    public class FileReader
    {
        public Dictionary<int, Dictionary<int, double>> Parse(char delimiter, string path)
        {
            var entries = new Dictionary<int, Dictionary<int, double>>();

            try
            {
                var lines = File.ReadAllLines(path)
                .Select(l => l.Split(delimiter).ToList());

                foreach (var line in lines)
                {
                    var user = int.Parse(line[0].Trim());
                    var id = int.Parse(line[1].Trim());
                    var rating = double.Parse(line[2].Trim(), CultureInfo.InvariantCulture);

                    if (entries.ContainsKey(user))
                    {
                        entries[user].Add(id, rating);
                    } else
                    {
                        entries.Add(user, new Dictionary<int, double> { { id, rating } });
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine("\nInvalid file path...");
                Console.ReadKey();
                System.Environment.Exit(0);
            }

            return entries;
        }
    }
}
