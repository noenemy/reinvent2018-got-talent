using GotTalent_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GotTalent_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<StageLog> StageLog { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<GameResult> GameResult { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // To support composite key for StageLog table
            modelBuilder.Entity<StageLog>()
                .HasKey(c => new {c.game_id, c.action_type});
        }
    }
}