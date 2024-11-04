using api_client_counter.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_client_counter.Db
{
    public class AppContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Founder> Founders { get; set; }
        public AppContext(DbContextOptions<AppContext> options) : base(options) => Database.EnsureCreated();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Founder>()
                .HasOne(s => s.Client)
                .WithMany(s => s.Founders)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}