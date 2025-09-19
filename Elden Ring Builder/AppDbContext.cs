using Elden_Ring_Builder.models;
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

        public AppDbContext()
        {
            // Создаём базу, если её нет
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Строим конфигурацию только из JSON файлов
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetings.json", optional: false) // шаблон для GitHub
                .AddJsonFile("appsettings.Development.json", optional: true) // реальные секреты
                .Build();

            // Берём строку подключения из конфигурации
            var connectionString = configuration.GetConnectionString("DefaultDb");

            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
        }

    }
}