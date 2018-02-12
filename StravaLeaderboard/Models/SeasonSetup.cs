﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StravaLeaderboard.models
{
    public class Club
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClubID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
    }

    public class Season
    {
        public int SeasonID { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public string Keyword { get; set; }
        public int ClubID { get; set; }

        //reference members
        public ICollection<SeasonAthlete> SeasonAthletes { get; set; }
        public Club Club { get; set; }
    }

    //JOIN Table between Season and Athlete
    public class SeasonAthlete
    {
        public int SeasonAthleteID { get; set; }
        public int SeasonID { get; set; }
        public int AthleteID { get; set; }

        public Season Season { get; set; }
        public Athlete Athlete { get; set; }
    }
}
