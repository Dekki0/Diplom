using System;
using System.Collections.Generic;

namespace SchoolFeeding.Model.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? StudentId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public virtual Student? Student { get; set; }
}
