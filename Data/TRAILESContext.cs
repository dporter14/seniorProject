using System;
using Microsoft.EntityFrameworkCore;
using TRAILES.Models;

namespace TRAILES.Data
{
    public class TRAILESContext : DbContext
    {
        public TRAILESContext (DbContextOptions<TRAILESContext> options) : base (options)
        {
        }

        public DbSet<Cabin> Cabin {get; set;} 
        public DbSet<Event> Event {get; set;} 
        public DbSet<EventAttendance> EventAttendance {get; set;} 
        public DbSet<FacStaff> FacStaff {get; set;} 
        public DbSet<Student> Student {get; set;} 
    }
}