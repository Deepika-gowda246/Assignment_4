using System;
using System.Collections.Generic;

namespace StudentWebApi.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public int? StudentId { get; set; }

    public string? SubjectName { get; set; }

    public int? MarksObtained { get; set; }

    public int? MaximumMarks { get; set; }

    public virtual Student? Student { get; set; }
}
