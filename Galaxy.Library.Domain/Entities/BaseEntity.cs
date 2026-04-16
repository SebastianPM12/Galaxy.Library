using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; protected set;}
        public bool IsActive { get; protected set;}
        public DateTime CreatedAt { get; protected set; }

        public DateTime UpdateAt { get; protected set; }

        protected BaseEntity() { 
            Id = Guid.Empty;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        protected void Delete()
        {
            IsActive = false;
            UpdateAt = DateTime.UtcNow;
        }


    }
}
