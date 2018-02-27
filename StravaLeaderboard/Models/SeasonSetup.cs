using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StravaLeaderboard.models
{
    public class Club
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClubID { get; set; }
        public int? StravaClubID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
    }

    public class Season
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeasonID { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public string Keyword { get; set; }

        //foreign keys
        public int ClubID { get; set; }

        //reference members
        public ICollection<SeasonAthlete> SeasonAthletes { get; set; }
        public List<Season> Seasons { get; set; }
        public Club Club { get; set; }
    }

    //JOIN Table between Season and Athlete
    public class SeasonAthlete
    {
        public int SeasonAthleteID { get; set; }

        //foreign keys
        public int SeasonID { get; set; }
        public int AthleteID { get; set; }

        public Season Season { get; set; }
        public Athlete Athlete { get; set; }
    }
}
