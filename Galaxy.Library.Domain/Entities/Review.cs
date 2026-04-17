using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Library.Domain.Entities;

public class Review: BaseEntity
{
    public Guid UserId { get; private set; }
    public Rating Rating { get; private set; } = default!;
    public ReviewComment Comment { get; private set; } = default!;
    public DateTime ReviewDate { get; private set; }

    private Review() { }


}
