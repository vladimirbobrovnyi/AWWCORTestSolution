using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace AWWCORTestSolution.Models
{
    public class AdContext : DbContext
    {
        public DbSet<Ad> Ads { get; set; }

        public AdContext(DbContextOptions<AdContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=Advertisement; Username=postgres; Password=pgadmin");
        }
    }
}
