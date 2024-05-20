using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    // Models <--> VT
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relations
            modelBuilder.Entity<Actor_Movie>().HasKey(acmo => new
            {
                acmo.ActorId,
                acmo.MovieId
            });

            // Actor_Movie <-->> Actor
            object value = modelBuilder.Entity<Actor_Movie>()
                .HasOne(m=> m.Actor)
                .WithMany(acmo=> acmo.Actors_Movies);

        }

    }
}
