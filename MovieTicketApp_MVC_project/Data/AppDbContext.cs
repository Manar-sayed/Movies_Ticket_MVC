using Microsoft.EntityFrameworkCore;
using MovieTicketApp_MVC_project.Models;

namespace MovieTicketApp_MVC_project.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }
        public AppDbContext() { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   optionsBuilder
        //      .UseSqlServer("Data Source =.; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate=True; Application Intent = ReadWrite; Multi Subnet Failover=False");
        //    base.OnConfiguring(optionsBuilder);
        //}
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

        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
