using Newtonsoft.Json;
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
        //TODO: should eventually be auto-generated
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DayEventID { get; set; }
        public DateTime Date { get; set; }

        //foreign keys
        public int SeasonID { get; set; }

        //reference members
        public Season Season { get; set; }
        public ICollection<DayEventSegment> DayEventSegments { get; set; }
        public List<Activity> Activities { get; set; }
    }

    //JOIN Table between Event and Segment
    public class DayEventSegment
    {
        public int DayEventSegmentID { get; set; }

        //foreign keys
        public int DayEventID { get; set; }
        public int SegmentID { get; set; }

        //reference members (apparently i don't need this)
        [JsonIgnore]
        public DayEvent DayEvent { get; set; }
        [JsonIgnore]
        public Segment Segment { get; set; }
    }
}
