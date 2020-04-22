using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TRAILES.Models;

namespace TRAILES.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventAttendance> EventAttendances { get; set; }
    }
}
