using StravaLeaderboard.models;
using Microsoft.EntityFrameworkCore;

namespace StravaLeaderboard.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SeasonAthlete> SeasonAthletes { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<DayEvent> DayEvents { get; set; }
        public DbSet<EventSegment> EventSegments { get; set; }
        public DbSet<Segment> Segments { get; set; }

    }
}
