using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracker.Models
{
    public class TrackerContext:DbContext
    {

        public TrackerContext(DbContextOptions<TrackerContext> options):
            base(options)
        {

        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity=> {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
