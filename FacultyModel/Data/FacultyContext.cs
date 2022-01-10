using AvramDaiana_FacultyProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvramDaiana_FacultyProject.Data
{
    public class FacultyContext:DbContext
    {
        public FacultyContext(DbContextOptions<FacultyContext> options) : base(options)
        {
        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<Exam>().ToTable("Exams");
            modelBuilder.Entity<Professor>().ToTable("Professor");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Course>()
            .HasKey(c => new { c.SubjectID, c.ProfessorID });
        }
    }
}
