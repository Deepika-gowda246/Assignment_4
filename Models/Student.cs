using System;
using System.Collections.Generic;

namespace StudentWebApi.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Class { get; set; }

    public virtual ICollection<Subject> Subjects { get; } = new List<Subject>();
}
