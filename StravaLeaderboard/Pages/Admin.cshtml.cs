using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StravaLeaderboard.Data;
using StravaLeaderboard.models;
using StravaLeaderboard.API;
using StravaLeaderboard.Http;
using Microsoft.EntityFrameworkCore;

namespace StravaLeaderboard.Pages
{
    public class AdminModel : PageModel
    {
        
        private DataContext _db;
        public APITokens _apitokens { get; set; }
        public AdminModel(DataContext db, IOptions<APITokens> apitokens)
        {
            _db = db;
            _apitokens = apitokens.Value;
        }

        //[TempData]
        //public string Message { get; set; }

        [BindProperty]
        public int DayEventID { get; set; }

        [BindProperty]
        public int UserClubID { get; set; }

        public List<Club> ClubsSelection { get; set; }
        public List<Season> Seasons { get; set; }
        public List<DayEvent> DayEvents { get; set; }
        public List<EventSegment> EventSegments { get; set; }
        public List<Segment> Segments { get; set; }

        public async Task OnGetAsync(int? id)
        {
            ClubsSelection = await _db.Clubs.ToListAsync();
            Seasons = await _db.Seasons.ToListAsync();
            DayEvents = await _db.DayEvents.ToListAsync();
            EventSegments = await _db.EventSegments.ToListAsync();
            Segments = await _db.Segments.ToListAsync();
        }

        public List<JSONActivity> SegmentActivities = new List<JSONActivity>();
        public List<Segment> SegmentLeaderboard = new List<Segment>();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            EventSegments = await _db.EventSegments
                .Where(i => i.DayEventID == DayEventID)
                .AsNoTracking()
                .ToListAsync();

            //get club activities - limited to top 200
            List<JSONActivity> Activities = await GetStravaActivities();
            //parse out activites with <Club.Keyword> in title
            Activities = ParseActivities(Activities);

            for (int x = 0; x < EventSegments.Count; x++)
            {
                SegmentActivities = GetStravaSegmentData(EventSegments[x].SegmentID);
            }
            //TODO: only go to /leaderboard if there are no errors
            //TODO: asynchronously add athletes if successful here
            return RedirectToPage("/Leaderboard");
        }

        public List<JSONActivity> GetStravaSegmentData(int segment)
        {
            //TODO: put this all in a try-catch block

            ////TODO: save activities in SQLite
            ////SaveActivities(Activities);

            ////get Segment Leaderboard for list of segments
            ////TODO: Add segment to SQLite if does not exist
            //RAWResults SegmentEntries = GetSegmentEntries(segment);

            ////parse out the segment efforts where a user name match 
            ////exists between the effort and the activity above
            //Activities = ParseEntries(SegmentEntries, Activities);

            ////write leaderboard to Leaderboard.cshtml in ranked order
            ////TODO: Strava is not returning the json efforts in order of elapsed_time - launched a ticket
            return null;// WriteLeaderboard(Activities, segment);

        }

        private List<JSONActivity> WriteLeaderboard(List<JSONActivity> activities, int segment)
        {

            return (from activity in activities
                    where activity.Athlete.SegmentResults != null
                    orderby activity.Athlete.SegmentResults[0].Rank
                    select activity).ToList();
        }

        public async Task<List<JSONActivity>> GetStravaActivities()
        {
            var stravaClubID = _db.Clubs.Where(i => i.ClubID == UserClubID).Select(s => s.StravaClubID).First();
            string getUrl = string.Format("{0}/{1}/activities?per_page={2}&access_token={3}", Endpoints.Club, stravaClubID,200, _apitokens.Access_Token);
            string json = await Strava.Http.WebRequest.SendGetAsync(new Uri(getUrl));

            return Unmarshaller<List<JSONActivity>>.Unmarshal(json);
        }

        public List<JSONActivity> ParseActivities(List<JSONActivity> activities)
        {
            //TODO: get keyword from model
            List<JSONActivity> ParsedActivities = (from activity in activities
                                                   where activity.Name.ToLower().Contains("watopia")
                                                   select activity).ToList();

            return ParsedActivities;
        }

        //public async void SaveActivities(List<Activity> activities)
        //{

        //}

        private RAWResults GetSegmentEntries(int Segment)
        {
            Uri uri = new Uri("https://www.strava.com/api/v3/segments/" + Segment.ToString() + "/leaderboard?access_token=" +
                _apitokens.Access_Token + "&per_page=200&context_entries=0&club_id=238810&date_range=this_week");
            HttpWebRequest httpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            httpRequest.Method = "GET";

            String response;
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                Stream responseStream = httpResponse.GetResponseStream();
                StreamReader reader = new System.IO.StreamReader(responseStream);
                response = reader.ReadToEnd();
                // Close both streams.
                reader.Close();
                responseStream.Close();
            }

            RAWResults deserializedObject = JsonConvert.DeserializeObject<RAWResults>(response);

            return deserializedObject;
        }

        private List<JSONActivity> ParseEntries(RAWResults segmentEntries, List<JSONActivity> activities)
        {
            //int points = 10;
            int rankCounter = 1;

            foreach (var segmentEntry in segmentEntries.Entries)
            {

                foreach (JSONActivity activity in activities)
                {

                    if (segmentEntry.Athlete_name.ToLower().Equals(activity.Athlete.FirstName.ToLower() + " " + activity.Athlete.LastName.Substring(0, 1).ToLower() + "."))
                    {
                        activity.Athlete.SegmentResults = new List<JSONResults>
                            {
                                new JSONResults() {
                                    Rank = rankCounter,
                                    Start_date = segmentEntry.Start_date,
                                    Elapsed_time = segmentEntry.Elapsed_time
                                    //Points = points
                                }
                            };

                        //activity.Athlete.TotalPoints = activity.Athlete.TotalPoints + points;
                        ////TODO: if polka points then add
                        //activity.Athlete.PolkaPoints = activity.Athlete.PolkaPoints + points;
                        ////TODO: if green points then add
                        //activity.Athlete.GreenPoints = activity.Athlete.GreenPoints + points;
                        //points = (points == 0) ? 0 : points - 2;
                        //activity.Athlete.SegmentCount++;
                        rankCounter++;
                        break;
                    }
                }
            }

            return activities;
        }
    }
}
