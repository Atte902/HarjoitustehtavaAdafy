using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows_Phone_Silverlight.MatchDetailsJson
{
    class MatchEvent
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int EventMinute { get; set; }
        public int ElapsedSeconds { get; set; }
        public int TeamId { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string EventTypeIcon { get; set; }
        public string EventType { get; set; }
        public int EventTypeEnum { get; set; }
        public int PlayerId { get; set; }
        public object Player { get; set; }
        public string Identifier { get; set; }
        public object AssistPlayers { get; set; }
        public object AssistPlayerNames { get; set; }
        public object Modifier { get; set; }
        public object Score { get; set; }
        public bool IsGoal { get; set; }
    }
}
