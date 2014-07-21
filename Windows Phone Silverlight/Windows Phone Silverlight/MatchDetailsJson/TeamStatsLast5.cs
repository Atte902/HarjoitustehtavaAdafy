using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Phone_Silverlight.MatchDetailsJson
{
    class TeamStatsLast5
    {
        public Team2 Team { get; set; }
        public int GamesPlayed { get; set; }
        public int Won { get; set; }
        public int Tie { get; set; }
        public int Lost { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsAgainst { get; set; }
        public int Points { get; set; }
        public bool Highlight { get; set; }
    }
}
