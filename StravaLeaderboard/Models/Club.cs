using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StravaLeaderboard.models
{
    public partial class Club
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClubID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
    }
}
