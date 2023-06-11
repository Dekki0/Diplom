using System;
using System.Collections.Generic;

namespace SchoolFeeding.Model.Entities;

public partial class Visit
{
    public int StudentId { get; set; }

    public DateTime DateVisit { get; set; }

    public virtual Student Student { get; set; } = null!;
}
