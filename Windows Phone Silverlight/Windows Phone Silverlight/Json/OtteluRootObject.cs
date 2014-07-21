using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Phone_Silverlight.Json
{
    class OtteluRootObject
    {
        public int Id { get; set; }
        public object Round { get; set; }
        public int RoundNumber { get; set; }
        public string MatchDate { get; set; }
        public HomeTeam HomeTeam { get; set; }
        public AwayTeam AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int Status { get; set; }
        public int PlayedMinutes { get; set; }
        public object SecondHalfStarted { get; set; }
        public string GameStarted { get; set; }
        public List<object> MatchEvents { get; set; }
        public List<object> PeriodResults { get; set; }
        public bool OnlyResultAvailable { get; set; }
        public int Season { get; set; }
        public string Country { get; set; }
        public string League { get; set; }
    }
}
