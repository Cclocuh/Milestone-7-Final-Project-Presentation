using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilestoneConsoleApp
{
    public class PlayerStats : IComparable<PlayerStats>
    {
        public string Initials { get; set; }
        public int DifficultyLevel { get; set; }
        public TimeSpan TimeElapsed { get; set; }

        public PlayerStats(string Initials, int DifficultyLevel, TimeSpan TimeElapsed) 
        {
            this.Initials = Initials;
            this.DifficultyLevel = DifficultyLevel;
            this.TimeElapsed = TimeElapsed;
        }
        public int CompareTo(PlayerStats other)
        {
            if (other == null) 
                return 1;

            if (this.DifficultyLevel != other.DifficultyLevel) 
                return this.DifficultyLevel.CompareTo(other.DifficultyLevel);

            return this.TimeElapsed.CompareTo(other.TimeElapsed);
        }
    }
}
