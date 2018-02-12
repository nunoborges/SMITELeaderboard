using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace StravaLeaderboard.models
{


    public class JSONActivity
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
        public JSONAthlete Athlete { get; set; }
    }

    public class JSONAthlete
    {
        //Athlete details members
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri Profile { get; set; }

        public List<JSONResults> SegmentResults { get; set; }

        ////Segment Results
        //private int segmentCount = 0;
        //public int SegmentCount
        //{   get
        //    { return segmentCount; }
        //    set
        //    { segmentCount = value; }
        //}

        //private int greenPoints = 0;
        //public int GreenPoints
        //{
        //    get
        //    { return greenPoints; }
        //    set
        //    { greenPoints = value; }
        //}

        //private int polkaPoints = 0;
        //public int PolkaPoints
        //{
        //    get
        //    { return polkaPoints; }
        //    set
        //    { polkaPoints = value; }
        //}

        //private int totalPoints = 0;
        //public int TotalPoints
        //{
        //    get
        //    { return totalPoints; }
        //    set
        //    { totalPoints = value; }
        //}
    }

    public class JSONResults
    {
        public int SegmentID { get; set; }
        public int Rank { get; set; } = 0;
        public int Elapsed_time { get; set; }
        public DateTime Start_date { get; set; }

    }

    public class RAWResults
    {
        public int Entry_count { get; set; }

        //reference members
        public List<RAWEntries> Entries { get; set; }
    }

    public class RAWEntries
    {
        public string Athlete_name { get; set; }
        public int Elapsed_time { get; set; }
        public int Rank { get; set; }
        public DateTime Start_date { get; set; }
    }



}