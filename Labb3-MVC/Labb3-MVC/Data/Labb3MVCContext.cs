using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Labb3MVC.Models;

namespace Labb3MVC.Models
{
    public class Labb3MVCContext : DbContext
    {
        public Labb3MVCContext (DbContextOptions<Labb3MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Filmer> Filmer { get; set; }
        public DbSet<Salong> Salong { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Filmer>()
                .HasOne(x => x.Salong)
                .WithMany(y => y.Filmers);
        }

    }
}
