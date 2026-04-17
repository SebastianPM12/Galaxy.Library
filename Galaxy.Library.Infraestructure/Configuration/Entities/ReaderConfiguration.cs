using Galaxy.Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Infraestructure.Configuration.Entities
{
    public class ReaderConfiguration : BaseEntityConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.ToTable("Readers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(150);

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(p => p.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(150);
            });

            builder.OwnsOne(x => x.DocumentNumber, document =>
            {
                document.Property(p => p.Value)
                    .HasColumnName("DocumentNumber")
                    .IsRequired()
                    .HasMaxLength(20);
            });

            builder.HasIndex("Email").IsUnique();
            builder.HasIndex("DocumentNumber").IsUnique();

            builder.HasMany(x => x.Loans)
                .WithOne(x => x.Reader)
                .HasForeignKey(x => x.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Reservations)
                .WithOne(x => x.Reader)
                .HasForeignKey(x => x.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    
}
