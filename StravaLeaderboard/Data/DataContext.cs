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

    }
}
