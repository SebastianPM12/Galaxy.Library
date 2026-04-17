using Galaxy.Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Infraestructure.Configuration.Entities
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        { 
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreatedAt).IsRequired().HasMaxLength(100);           
            builder.Property(e => e.UpdateAt).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.IsActive).IsRequired().HasMaxLength(100);

        }
    }
}
