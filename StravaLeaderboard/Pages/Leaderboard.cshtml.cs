using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StravaLeaderboard.Data;
using StravaLeaderboard.models;

namespace StravaLeaderboard.Pages
{
    public class LeaderboardModel : PageModel
    {

        private DataContext _db;
        public APITokens _apitokens { get; set; }
        public LeaderboardModel(DataContext db, IOptions<APITokens> apitokens)
        {
            _db = db;
            _apitokens = apitokens.Value;
        }

        public List<EventSegment> EventSegments { get; set; }
        public List<Segment> Segments { get; set; }
        public List<Activity> Activities { get; set; }

        public int? DayEventID { get; set; }
        public async Task OnGetAsync(int? dayEventID)
        {
            //TODO: do something here if dayEventID is null or 0
            DayEventID = dayEventID;
            EventSegments = await _db.EventSegments
                .Where(s => s.DayEventID == DayEventID)
                .ToListAsync();



        }       
    }
}
