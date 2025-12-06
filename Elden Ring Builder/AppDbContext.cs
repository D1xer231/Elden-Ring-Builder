using Elden_Ring_Builder.models;
using Elden_Ring_Builder.telegram;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Elden_Ring_Builder
{
    internal class AppDbContext : DbContext
    {
        public DbSet<builds> Builds { get; set; }
        public DbSet<weapons> Weapons { get; set; }
        public DbSet<runes> Runes { get; set; }

        public DbSet<gallery> Gallery { get; set; }
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

            try
            {
                options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
            }catch(MySqlConnector.MySqlException ex)
            {
                MessageBox.Show("Error during connection to db. Check connection settings.\n" + ex.Message, "Elden Ring Builder - Error", MessageBoxButton.OK, MessageBoxImage.Error);
                string user_input = Interaction.InputBox(
                    "Tell us if u find any problems, Thank You!",
                    "Elden Ring Builder - Report",
                    ""
                );
                if (user_input.Length < 5 || user_input == null || user_input == "")
                {
                    MessageBox.Show("No message was sent", "Elden Ring Builder - Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = $"mailto:aalexandr397@gmail.com?subject=AppReport&body={user_input}",
                        UseShellExecute = true
                    });
                }
                    MessageBox.Show("Application will be closed.", "Elden Ring Builder - Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(0);
            }
        }

    }
}