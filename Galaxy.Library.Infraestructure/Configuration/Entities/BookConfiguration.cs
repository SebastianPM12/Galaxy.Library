using Galaxy.Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Infraestructure.Configuration.Entities
{
    public class BookConfiguration : BaseEntityConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Author)
                .IsRequired()
                .HasMaxLength(150);


            builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(250);


            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.Property(x => x.TotalCopies)
                .IsRequired();

            builder.Property(x => x.AvailableCopies)
                .IsRequired();

            builder.OwnsOne(x => x.Isbn, isbn =>
            {
                isbn.Property(p => p.Value)
                    .HasColumnName("Isbn")
                    .IsRequired()
                    .HasMaxLength(13);
            });

            builder.HasIndex("Isbn").IsUnique();

            builder.HasOne(x => x.Category)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Loans)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Reservations)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
