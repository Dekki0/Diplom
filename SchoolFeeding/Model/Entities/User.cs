using System;
using System.Collections.Generic;

namespace SchoolFeeding.Model.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
