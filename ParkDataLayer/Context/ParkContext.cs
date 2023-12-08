using Microsoft.EntityFrameworkCore;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Context
{
    public class ParkContext : DbContext
    {
        public DbSet<HuurderEF> Huurders { get; set; }
        public DbSet<HuisEF> Huizen { get; set; }
        public DbSet<HuurcontractEF> Huurcontracten { get; set; }
        public DbSet<ParkEF> Parken { get; set; } 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DANIEL\SQLEXPRESS;Initial Catalog=ParkBeheerdb;Integrated Security=True;Pooling=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HuisEF>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<HuurderEF>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<HuurcontractEF>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<HuisEF>()
                .HasMany(h => h._huurcontracten)
                .WithOne();

            modelBuilder.Entity<HuisEF>()
                .HasMany(h => h._huurders)
                .WithOne(); 

            modelBuilder.Entity<HuurcontractEF>()
                .HasOne(hc => hc.Huurder)
                .WithOne();

            modelBuilder.Entity<ParkEF>()
                .HasMany(p => p._huis)
                .WithOne();

        }


    }
}
