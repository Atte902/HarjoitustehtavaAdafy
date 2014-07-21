using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Phone_Silverlight.MatchDetailsJson
{
    class News
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string DateTime { get; set; }
        public string Url { get; set; }
        public string OriginalUrl { get; set; }
        public string Source { get; set; }
        public string Content { get; set; }
        public int Id { get; set; }
        public object ImageUrl { get; set; }
        public List<object> RelatedTeams { get; set; }
    }
}
