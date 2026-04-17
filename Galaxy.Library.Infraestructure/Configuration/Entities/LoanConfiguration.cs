using Galaxy.Library.Domain.Entities;
using Galaxy.Library.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Infraestructure.Configuration.Entities
{
    public class LoanConfiguration : BaseEntityConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");

            builder.Property(x => x.BookId)
                .IsRequired();

            builder.Property(x => x.ReaderId)
                .IsRequired();

            builder.Property(x => x.LoanDate)
                .IsRequired();

            builder.Property(x => x.DueDate)
                .IsRequired();

            builder.Property(x => x.ReturnDate);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<LoanStatus>(v))
                .HasMaxLength(20);


            builder.HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Reader)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Penalty)
                .WithOne(x => x.Loan)
                .HasForeignKey<Penalty>(x => x.LoanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
