using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentWebApi.Models;

public partial class StudentsDatabaseContext : DbContext
{
    

    public StudentsDatabaseContext(DbContextOptions<StudentsDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52A79F95BF1CA");

            entity.ToTable("Student");

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Class)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA388472AD5B1");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.MarksObtained).HasColumnName("Marks_obtained");
            entity.Property(e => e.MaximumMarks)
                .HasDefaultValueSql("((100))")
                .HasColumnName("Maximum_marks");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Subjects__Studen__36B12243");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
