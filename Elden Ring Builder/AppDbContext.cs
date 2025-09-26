using Elden_Ring_Builder.models;
using Elden_Ring_Builder.telegram;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Elden_Ring_Builder
{
    internal class AppDbContext : DbContext
    {
        public DbSet<builds> Builds { get; set; }
        public DbSet<weapons> Weapons { get; set; }
        public DbSet<runes> Runes { get; set; }
        //public DbSet<requests> Applications { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultDb");

            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
        }

    }
}