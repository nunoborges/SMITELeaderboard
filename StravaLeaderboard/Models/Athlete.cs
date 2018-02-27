using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public class Athlete
    {
        //activity members
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AthleteID { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Profile { get; set; }
        public string Access_token { get; set; }
        public DateTime Date_joined { get; set; }

        //reference members
        public ICollection<SeasonAthlete> SeasonAthletes { get; set; }
    }
}
