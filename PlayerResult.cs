using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace snakeBohatyrov
{
    class PlayerResult
    {

        public string Name { get; set; }
        public int Score { get; set; }

    }


    class PlayerResultsManager
    {
        public void SaveResultsToFile(List<PlayerResult> results, string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (PlayerResult result in results)
                {
                    writer.WriteLine($"{result.Name},{result.Score}");
                }
            }
        }

        public List<PlayerResult> LoadResultsFromFile(string filename)
        {
            List<PlayerResult> results = new List<PlayerResult>();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 2)
                    {
                        string name = parts[0];
                        int score = int.Parse(parts[1]);

                        results.Add(new PlayerResult { Name = name, Score = score });
                    }
                }
            }

            // Упорядочивание результатов в порядке убывания
            results.Sort((r1, r2) => r2.Score.CompareTo(r1.Score));

            return results;
        }
    }
}