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
        public DbSet<Game> Game { get; set; }
        public DbSet<Casting> Casting { get; set; }
        public DbSet<GameResult> GameResult { get; set; }
    }
}