﻿using StravaLeaderboard.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class Segment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SegmentID { get; set; }
        public string Name { get; set; }       
        public string Type { get; set; }
        public string World { get; set; }
        public int Distance { get; set; }
        public int Average_grade { get; set; }
        public int Total_elevation_gain { get; set; }
        public int Star_count { get; set; }

        //reference members
        public ICollection<DayEventSegment> DayEventSegments { get; set; }
        public List<ActivityResult> ActivityResults { get; set; }
    }
}
