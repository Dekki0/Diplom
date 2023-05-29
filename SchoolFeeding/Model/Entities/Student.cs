using System;
using System.Collections.Generic;

namespace SchoolFeeding.Model.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? ClassId { get; set; }

    public virtual Balance? Balance { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
