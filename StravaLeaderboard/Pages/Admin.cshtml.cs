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

namespace StravaLeaderboard.Pages
{
    public partial class AdminModel : PageModel
    {
        
        private readonly DataContext _db;
        public APITokens _apitokens { get; set; }
        public AdminModel(DataContext db, IOptions<APITokens> apitokens)
        {
            _db = db;
            _apitokens = apitokens.Value;
        }

        [TempData]
        //public string Message { get; set; }

        [BindProperty]
        public Club club { get; set; }

        // London = 14063868,13619366 (tim's tongue twister)
        // Watopia = 16730849,16730862,16730897,16730888,16936841,16730909
        // watopia SMITE Feb 10 = 13855855,14485439,13521759,14250115
        public static int[] segments = new int[] { 14063868, 13619366 };
        public List<JSONActivity> SegmentActivities = new List<JSONActivity>();
        public List<Segment> SegmentLeaderboard = new List<Segment>();

        public void OnGet()
        {

            for (int x = 0; x < segments.Length; x++)
            {
                SegmentActivities = FetchStravaData(segments[x]);
                SegmentLeaderboard.Add(new Segment()
                {
                    Name = "SMITE: blah" + x.ToString(),
                    SegmentID = 2342343,
                    Type = "Green",
                    World = "Watopia"
                });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Clubs.Add(club);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }

        public List<JSONActivity> FetchStravaData(int segment)
        {
            //get club activities - limited to top 200
            //TODO: don't need to get Activities multiple times
            List<JSONActivity> Activities = GetActivities();

            //parse out activites with "SMITE" in title
            //TODO: don't need to do this multiple times
            Activities = ParseActivities(Activities);

            //TODO: save activities in SQLite
            //SaveActivities(Activities);

            //get Segment Leaderboard for list of segments
            //TODO: Add segment to SQLite if does not exist
            RAWResults SegmentEntries = GetSegmentEntries(segment);

            //parse out the segment efforts where a user name match 
            //exists between the effort and the activity above
            Activities = ParseEntries(SegmentEntries, Activities);

            //write leaderboard to Leaderboard.cshtml in ranked order
            //TODO: Strava is not returning the json efforts in order of elapsed_time - launched a ticket
            return WriteLeaderboard(Activities, segment);

        }

        private List<JSONActivity> WriteLeaderboard(List<JSONActivity> activities, int segment)
        {

            return (from activity in activities
                    where activity.Athlete.SegmentResults != null
                    orderby activity.Athlete.SegmentResults[0].Rank
                    select activity).ToList();
        }

        public List<JSONActivity> GetActivities()
        {
            Uri uri = new Uri("https://www.strava.com/api/v3/clubs/238810/activities?access_token=" +
            _apitokens.Access_Token +
            "&per_page=200");

            HttpWebRequest httpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            httpRequest.Method = "GET";

            String response;
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                Stream responseStream = httpResponse.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                response = reader.ReadToEnd();

                // Close both streams.
                reader.Close();
                responseStream.Close();

                //return response;
            }

            List<JSONActivity> deserializedObject = JsonConvert.DeserializeObject<List<JSONActivity>>(response);

            return deserializedObject;
        }

        public List<JSONActivity> ParseActivities(List<JSONActivity> activities)
        {
            List<JSONActivity> ParsedActivities = (from activity in activities
                                                   where activity.Name.ToLower().Contains("london")
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
