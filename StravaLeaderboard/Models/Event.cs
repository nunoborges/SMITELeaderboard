using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class Event
    {
        //activity members
        public int EventID { get; set; }
        public DateTime Date { get; set; }
        public int SeasonID { get; set; }

        //reference members
        public Season Season { get; set; }
    }

    //JOIN Table between Event and Segment
    public class EventSegment
    {
        public int EventSegmentID { get; set; }

        //reference members
        public Event Event { get; set; }
        public Segment Segment { get; set; }
    }
}
