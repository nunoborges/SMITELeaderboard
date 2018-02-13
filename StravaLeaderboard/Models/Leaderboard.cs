using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class SeasonLeaderboard
    {
        public int SeasonLeaderboardID { get; set; }
        public int Total_points { get; set; }
        public int Total_adj_points { get; set; }
        public int Total_green_points { get; set; }
        public int Total_polka_points { get; set; }
        public DateTime Date_started { get; set; }
        public int Events_attended { get; set; }

        //foreign keys
        public int SeasonID { get; set; }
        public int AthletID { get; set; }

        //reference members
        public Season Season { get; set; }
        public Athlete Athlete { get; set; }

    }
}
