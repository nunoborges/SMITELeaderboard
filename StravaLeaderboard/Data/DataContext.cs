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
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Athlete> Athletes { get; set; }

        public DbSet<SegmentLeaderboard> SegmentLeaderboard { get; set; }
        public DbSet<SegmentEntries> SegmentEntries { get; set; }
        public DbSet<Entries> Entries { get; set; }
    }
}
