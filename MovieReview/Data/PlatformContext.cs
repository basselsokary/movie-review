using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieReview.Models.Entities;

namespace MovieReview.Data;

public class PlatformContext : IdentityDbContext<AppUser>
{
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Genres> Genres { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }
    public DbSet<Watchlist> Watchlists { get; set; }

    public PlatformContext(DbContextOptions<PlatformContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure many-to-many relationship
        modelBuilder.Entity<MovieActor>(ma =>
        {
            ma.HasKey(ma => new { ma.MovieId, ma.ActorId }); // Composite Key
            
            ma.HasOne(ma => ma.Movie)
              .WithMany(m => m.MovieActors)
              .HasForeignKey(ma => ma.MovieId);

            ma.HasOne(ma => ma.Actor)
              .WithMany(a => a.MovieActors)
              .HasForeignKey(ma => ma.ActorId);
        });

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = "1",
                Name = Static.UserRoles.Admin,
                NormalizedName = Static.UserRoles.Admin.ToUpper()
            },
            new IdentityRole
            {
                Id = "2",
                Name = Static.UserRoles.User,
                NormalizedName = Static.UserRoles.User.ToUpper()
            },
        };
        modelBuilder.Entity<IdentityRole>().HasData(roles);
    
        base.OnModelCreating(modelBuilder);
    }
}
