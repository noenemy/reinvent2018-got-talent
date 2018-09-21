using GotTalent_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GotTalent_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<UserScore> UserScore { get; set; }
        public DbSet<StageLog> StageLog { get; set; }
        public DbSet<Ranking> Ranking { get; set; }
    }
}