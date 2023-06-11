using System;
using System.Collections.Generic;

namespace SchoolFeeding.Model.Entities;

public partial class Balance
{
    public int StudentId { get; set; }

    public decimal Balance1 { get; set; }

    public virtual Student Student { get; set; } = null!;
    public override string ToString() => Balance1.ToString();
}
