using Elden_Ring_Builder.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Elden_Ring_Builder
{
    internal class AppDbContext : DbContext
    {
        public DbSet<builds> Builds { get; set; }
        public DbSet<weapons> Weapons { get; set; }
        public DbSet<runes> Runes { get; set; }
        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = "server=localhost;port=3306;username=root;password=root;database=elden_ring_builder";

            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
        }
    }
}
