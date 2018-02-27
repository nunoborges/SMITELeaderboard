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
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityResult> ActivityResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Club>().ToTable("Clubs");
            modelBuilder.Entity<Season>().ToTable("Seasons");
            modelBuilder.Entity<SeasonAthlete>().ToTable("SeasonAthletes");
            modelBuilder.Entity<Athlete>().ToTable("Athletes");
            modelBuilder.Entity<DayEvent>().ToTable("DayEvents");
            modelBuilder.Entity<EventSegment>().ToTable("EventSegments");
            modelBuilder.Entity<Segment>().ToTable("Segments");
            modelBuilder.Entity<Activity>().ToTable("Activities");
            modelBuilder.Entity<ActivityResult>().ToTable("ActivityResults");
        }
    }
}
