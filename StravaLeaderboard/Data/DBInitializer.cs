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

            // Look for any students.
            if (context.Clubs.Any())
            {
                return;   // DB has been seeded
            }

            var clubs = new Club[]
            {
                new Club{ClubID=1,Name="SMITE",Location="Volcano", Country="Watopia"},
                new Club{ClubID=2,Name="FUCCIT",Location="Uxbridge", Country="Canada"}
            };
            
            foreach (Club club in clubs)
            {
                context.Clubs.Add(club);
            }
            context.SaveChanges();


        }
    }
}