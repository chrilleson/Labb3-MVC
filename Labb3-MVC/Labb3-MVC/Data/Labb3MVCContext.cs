using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Labb3MVC.Models
{
    public class Labb3MVCContext : DbContext
    {
        public Labb3MVCContext (DbContextOptions<Labb3MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Labb3MVC.Models.Filmer> Filmer { get; set; }
    }
}
