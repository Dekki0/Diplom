using System;
using System.Collections.Generic;

namespace SchoolFeeding.Model.Entities;

public partial class Report
{
    public int ReportId { get; set; }

    public string ReportType { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public string? ReportContent { get; set; }
}
