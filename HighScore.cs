using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilestoneConsoleApp
{
    public class HighScore
    {
        
        private static readonly string FileName = "highscores.csv";


        public static List<PlayerStats> LoadHighScores (int difficultyLevel)
        {
            List<PlayerStats > highScores = new List<PlayerStats> ();

            if (!File.Exists (FileName))
                return highScores;

            using (var reader = new StreamReader(FileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (int.TryParse(values[1], out int level) &&
                        level == difficultyLevel && TimeSpan.TryParse(values[2], out TimeSpan time))
                    {
                        /*highScores.Add(new PlayerStats
                        {
                            Initials = values[0],
                            DifficultyLevel = level,
                            TimeElapsed = time
                        });*/

                    }
                }
            }
            return highScores.OrderByDescending(s => s.TimeElapsed).ToList();
        }

        public static void SaveHighScore(PlayerStats stats)
        {
            using (var writer = new StreamWriter(FileName, true))
            {
                writer.WriteLine($"{stats.Initials},{stats.DifficultyLevel},{stats.TimeElapsed}");
            }
        }
    }
}
