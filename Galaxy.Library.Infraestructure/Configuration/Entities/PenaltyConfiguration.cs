using Galaxy.Library.Domain.Entities;
using Galaxy.Library.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Infraestructure.Configuration.Entities
{
    public class PenaltyConfiguration : BaseEntityConfiguration<Penalty>
    {
        public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder.ToTable("Penalties");


            builder.Property(x => x.LoanId)
                .IsRequired();

            builder.OwnsOne(x => x.Amount, money =>
            {
                money.Property(p => p.Value)
                    .HasColumnName("Amount")
                    .IsRequired()
                    .HasColumnType("decimal(10,2)");
            });

            builder.Property(x => x.Reason)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<PenaltyStatus>(v))
                .HasMaxLength(20);


            builder.HasOne(x => x.Loan)
                .WithOne(x => x.Penalty)
                .HasForeignKey<Penalty>(x => x.LoanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
