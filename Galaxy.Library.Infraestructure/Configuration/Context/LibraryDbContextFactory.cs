using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Galaxy.Library.Infraestructure.Configuration.Context
{
    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var devConnectionString = "Host=localhost;Port=1502;Database=bd_library;Username=admin;Password=Password2025;";
            var connectionString = Environment.GetEnvironmentVariable("dbLibrary") ?? devConnectionString;

            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();

            optionsBuilder.UseNpgsql(connectionString, options => 
            {
                options.MigrationsHistoryTable("__EFMigrationsHistory", "library");
            });

            return new LibraryDbContext(optionsBuilder.Options);
        }
    }
}
