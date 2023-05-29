using System;
using System.Collections.Generic;

namespace SchoolFeeding.Model.Entities;

public partial class Class
{
    public int ClassId { get; set; }

    public string ClassName { get; set; } = null!;

    public byte ClassCode { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
