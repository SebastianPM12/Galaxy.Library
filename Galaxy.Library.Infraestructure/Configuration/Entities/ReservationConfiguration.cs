using Galaxy.Library.Domain.Entities;
using Galaxy.Library.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Infraestructure.Configuration.Entities
{
    public class ReservationConfiguration : BaseEntityConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");


            builder.Property(x => x.BookId)
                .IsRequired();

            builder.Property(x => x.ReaderId)
                .IsRequired();

            builder.Property(x => x.ReservationDate)
                .IsRequired();

            builder.Property(x => x.ExpirationDate)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<ReservationStatus>(v))
                .HasMaxLength(20);

          
            builder.HasOne(x => x.Book)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Reader)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
