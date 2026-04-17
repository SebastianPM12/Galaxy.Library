using Galaxy.Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Infraestructure.Configuration.Context
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Loan> Loans { get; set; } = default!;
        public DbSet<Penalty> Penalties { get; set; } = default!;
        public DbSet<Reservation> Reservations { get; set; } = default!;
        public DbSet<Reader> Readers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
        }

    }
}
