using Microsoft.EntityFrameworkCore;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(a => new
            {
                a.MovieId,
                a.ActorId
            });
            modelBuilder.Entity<Actor_Movie>()
                .HasOne(a => a.Movie)
                .WithMany(a => a.Actors_Movies)
                .HasForeignKey(a => a.MovieId);  
            modelBuilder.Entity<Actor_Movie>()
                .HasOne(a => a.Actor)
                .WithMany(a => a.Actors_Movies)
                .HasForeignKey(a => a.ActorId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas{ get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
    }
}
