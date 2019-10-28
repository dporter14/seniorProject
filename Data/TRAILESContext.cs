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

            public DbSet<Cabin> Cabin {get; set;}
    }
}