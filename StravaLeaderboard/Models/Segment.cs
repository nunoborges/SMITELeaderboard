using StravaLeaderboard.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class Segment
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Type { get; set; }
        public string World { get; set; }
        public List<JSONActivity> Activity { get; set; }
        //TODO: add foreign key to Event
    }
}
