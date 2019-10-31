using Microsoft.EntityFrameworkCore;
using TRAILES.Models;

namespace TRAILES.Data
{
    public class TRAILESContext : DbContext
    {
        public TRAILESContext (DbContextOptions<TRAILESContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne<Cabin>(s => s.Cabin)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.CabinId)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Cabin> Cabin {get; set;}

        public DbSet<User> User {get; set;}
    }
}