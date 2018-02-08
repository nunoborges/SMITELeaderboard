using StravaLeaderboard.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class SegmentLeaderboard
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Type { get; set; }
        public string World { get; set; }
        public List<Activity> Activity { get; set; }
    }
}
