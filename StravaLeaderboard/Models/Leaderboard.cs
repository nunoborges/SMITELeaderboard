using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class SegmentResults
    {
        public int SegmentLeaderboardID { get; set; }
        public string SegmentType { get; set; }
        public int Rank { get; set; } = 0;
        public int Elapsed_time { get; set; }
        public DateTime Start_date { get; set; }

        //calculated
        public int Points { get; set; }
    }
}
