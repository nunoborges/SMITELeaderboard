using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class Activity
    {
        //activity members
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start_date { get; set; }
        public int Achievement_count { get; set; }
        public int Comment_count { get; set; }
        public int Kudos_count { get; set; }
        public Boolean Flagged { get; set; }

        //reference members
        //public JSONAthlete Athlete { get; set; }
    }
}
