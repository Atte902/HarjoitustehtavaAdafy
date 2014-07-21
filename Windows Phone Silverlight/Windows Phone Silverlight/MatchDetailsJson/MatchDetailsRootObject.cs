using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Phone_Silverlight.MatchDetailsJson
{
    class MatchDetailsRootObject
    {
        public Match Match { get; set; }
        public List<News> News { get; set; }
        public List<Line> Lines { get; set; }
        public List<TeamStatsLast5> TeamStatsLast5 { get; set; }
        public List<TeamStatsLast10> TeamStatsLast10 { get; set; }
        public List<MatchAction> MatchActions { get; set; }
    }
}
