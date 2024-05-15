using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MusicStarStore.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base(options) 
        {
        

        }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<VinylGenre> VinylGenre { get; set; }

        public DbSet<Vinyl> Vinyl { get; set; }
    }
}
