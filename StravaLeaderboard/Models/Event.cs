using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class DayEvent
    {
        //activity members
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DayEventID { get; set; }
        public DateTime Date { get; set; }

        //foreign keys
        public int SeasonID { get; set; }

        //reference members
        public Season Season { get; set; }
        public ICollection<EventSegment> EventSegments { get; set; }
        public List<Activity> Activities { get; set; }
    }

    //JOIN Table between Event and Segment
    public class EventSegment
    {
        public int EventSegmentID { get; set; }

        //foreign keys
        public int DayEventID { get; set; }
        public int SegmentID { get; set; }

        //reference members
        public DayEvent Event { get; set; }
        public Segment Segment { get; set; }
    }
}
