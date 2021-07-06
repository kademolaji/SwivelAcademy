using Microsoft.EntityFrameworkCore;
using Swivel.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swivel.Database
{
    public partial class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
        public virtual DbSet<TeacherCourse> TeacherCourse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => e.Id)
                         .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasDatabaseName("IX_CourseName")
                    .IsUnique();
            });
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasIndex(e => e.Id)
                         .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasDatabaseName("IX_StudentEmail")
                    .IsUnique();

                entity.HasIndex(e => e.FirstName)
                    .HasDatabaseName("IX_StudentFirstName");

                entity.HasIndex(e => e.LastName)
                    .HasDatabaseName("IX_StudentLastName");
            });
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasIndex(e => e.Id)
                         .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasDatabaseName("IX_TeacherEmail")
                    .IsUnique();

                entity.HasIndex(e => e.FirstName)
                                   .HasDatabaseName("IX_TeacherFirstName");

                entity.HasIndex(e => e.LastName)
                    .HasDatabaseName("IX_TeacherLastName");
            });
            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId });
                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Students_StudentId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TeacherCourse>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.CourseId });
                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherCourse_Teachers_TeacherId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
