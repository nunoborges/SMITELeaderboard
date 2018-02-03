using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StravaLeaderboard.models;

namespace StravaLeaderboard.Pages
{
    public class LeaderboardModel : PageModel
    {
        //https://codingblast.com/asp-net-core-configuration-reloading-binding-injecting/
        //https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/sort-filter-page
        public LeaderboardModel(IOptions<APITokens> apitokens)
        {
            _apitokens = apitokens.Value;
        }  
        
        public string Message { get; set; }
        public APITokens _apitokens { get; set; }

        public void OnGet()
        {
            Message = "Your Strava Access_Token = " +
                _apitokens.Access_Token;
            List<Activity> Activities = FetchStravaData();
        }

        public List<Activity> FetchStravaData()
        {
            //get club activities - limited to top 200
            List<Activity> Activities = GetActivities();

            //parse out activites with "SMITE" in title
            Activities = ParseActivities(Activities);

            //save activities in SQLite
            SaveActivities(Activities);

            //get Segment Leaderboard for list of segments
            //TODO: go to contentful to get the segment list and iterate through each
            SegmentEntries SegmentEntries = GetSegmentEntries(13619390);

            //parse out the segment efforts where a user name match 
            //exists between the effort and the activity above
            Activities = ParseEntries(SegmentEntries, Activities);

            //write leaderboard to console in ranked order
            //TODO: Strava is not returning the json efforts in order of elapsed_time - launched a ticket
            Activities = WriteLeaderboard(Activities);

            return Activities;
        }

        private List<Activity> WriteLeaderboard(List<Activity> activities)
        {
            //try ordering activities with order by
            List<Activity> sortedActivities = (from activity in activities
                                               where activity.Athlete.SegmentLeaderboard != null
                                               orderby activity.Athlete.SegmentLeaderboard[0].Rank
                                               select activity).ToList();

            foreach (Activity activity in sortedActivities)
            {
                Console.WriteLine(activity.Athlete.FirstName + " " + activity.Athlete.LastName);
                Console.WriteLine("Rank: " + activity.Athlete.SegmentLeaderboard[0].Rank);
                Console.WriteLine("Points: " + activity.Athlete.SegmentLeaderboard[0].Points);
                Console.WriteLine("Elapsed Time: " + activity.Athlete.SegmentLeaderboard[0].Elapsed_time);
                //Console.WriteLine("Start Time: " + activity.AthleteID.SegmentLeaderboard[0].Start_date);
                Console.WriteLine();
            }
            return sortedActivities;
        }

        public List<Activity> GetActivities()
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

            List<Activity> deserializedObject = JsonConvert.DeserializeObject<List<Activity>>(response);

            return deserializedObject;
        }

        public List<Activity> ParseActivities(List<Activity> activities)
        {
            List<Activity> ParsedActivities = (from activity in activities
                                               where activity.Name.ToLower().Contains("watopia")
                                               select activity).ToList();

            return ParsedActivities;
        }

        public async void SaveActivities(List<Activity> activities)
        {

        }

        private SegmentEntries GetSegmentEntries(int Segment)
        {
            Uri uri = new Uri("https://www.strava.com/api/v3/segments/" + Segment + "/leaderboard?access_token=" +
                _apitokens.Access_Token +"&per_page=200&context_entries=0&club_id=238810&date_range=this_week");
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

            SegmentEntries deserializedObject = JsonConvert.DeserializeObject<SegmentEntries>(response);

            return deserializedObject;
        }

        private List<Activity> ParseEntries(SegmentEntries segmentEntries, List<Activity> activities)
        {
            int points = 10;
            int rankCounter = 1;

            foreach (var segmentEntry in segmentEntries.Entries)
            {

                foreach (Activity activity in activities)
                {

                    if (segmentEntry.Athlete_name.ToLower().Equals(activity.Athlete.FirstName.ToLower() + " " + activity.Athlete.LastName.Substring(0, 1).ToLower() + "."))
                    {
                        activity.Athlete.SegmentLeaderboard = new List<SegmentLeaderboard>
                            {
                                new SegmentLeaderboard() {
                                    Rank = rankCounter,
                                    Start_date = segmentEntry.Start_date,
                                    Elapsed_time = segmentEntry.Elapsed_time,
                                    Points = points
                                }
                            };

                        activity.Athlete.TotalPoints = activity.Athlete.TotalPoints + points;
                        //TODO: if polka points then add
                        activity.Athlete.PolkaPoints = activity.Athlete.PolkaPoints + points;
                        //TODO: if green points then add
                        activity.Athlete.GreenPoints = activity.Athlete.GreenPoints + points;
                        points = (points == 0) ? 0 : points - 2;
                        activity.Athlete.SegmentCount++;
                        rankCounter++;
                        break;
                    }
                }
            }

            return activities;
        }
    }
}
