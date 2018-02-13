using StravaLeaderboard.models;
using System;
using System.Linq;

namespace StravaLeaderboard.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any clubs.
            if (context.Clubs.Any())
            {
                return;   // DB has been seeded
            }

            var clubs = new Club[]
            {
                new Club{ClubID=1,ZwiftID=238810,Name="SMITE",Location="Volcano", Country="Watopia"},
                new Club{ClubID=2,Name="SMITE-London",Location="Radio Tower", Country="Watopia"},
                new Club{ClubID=3,Name="SMITE-Watopia",Location="Hank's House", Country="Watopia"}
            };
            
            foreach (Club club in clubs)
            {
                context.Clubs.Add(club);
            }
            context.SaveChanges();

            
            // Look for any seasons.
            if (context.Seasons.Any())
            {
                return;   // DB has been seeded
            }

            var seasons = new Season[]
            {
                new Season{SeasonID=1,ClubID=1,Start_date=DateTime.Now,End_date=DateTime.Now, Keyword="smite"},
                new Season{SeasonID=2,ClubID=2,Start_date=DateTime.Now,End_date=DateTime.Now, Keyword="london"},
                new Season{SeasonID=3,ClubID=3,Start_date=DateTime.Now,End_date=DateTime.Now, Keyword="watopia"}
            };

            foreach (Season season in seasons)
            {
                context.Seasons.Add(season);
            }
            context.SaveChanges();


            // Look for any events.
            if (context.DayEvents.Any())
            {
                return;   // DB has been seeded
            }

            var dayevents = new DayEvent[]
            {
                new DayEvent{DayEventID=1,Date=DateTime.Now,SeasonID=1},
                new DayEvent{DayEventID=2,Date=DateTime.Now,SeasonID=2},
                new DayEvent{DayEventID=3,Date=DateTime.Now,SeasonID=3}
            };

            foreach (DayEvent dayevent in dayevents)
            {
                context.DayEvents.Add(dayevent);
            }
            context.SaveChanges();
        }
    }
}