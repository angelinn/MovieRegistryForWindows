using Microsoft.EntityFrameworkCore;
using MovieRegistry.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRegistry.Models
{
    public class MovieRegistryContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<WindowsUser> Users { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Registry.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
