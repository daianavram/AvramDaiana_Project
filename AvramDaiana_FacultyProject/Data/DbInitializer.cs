using AvramDaiana_FacultyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvramDaiana_FacultyProject.Data;

namespace AvramDaiana_FacultyProject.Data
{
    public class DbInitializer
    {
        public static void Initialize(FacultyContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;
            }
            var students = new Student[]
            {
                new Student{Name="Avram Daiana",Faculty="E-Business",Year=1},
                new Student{Name="Popescu Iulian",Faculty="Marketing",Year=2},
                new Student{Name="Georgescu Maria",Faculty="Contabilitate",Year=1},
                new Student{Name="Toma Catalin",Faculty="Contabilitate",Year=3}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var subjects = new Subject[]
            {

                new Subject{SubjectID=1050,Name="Medii de programare avansate",Credits=4},
                new Subject{SubjectID=1045,Name="SIMPA",Credits=3},
                new Subject{SubjectID=1040,Name="Managementul proiectelor",Credits=4},

            };
            foreach (Subject c in subjects)
            {
                context.Subjects.Add(c);
            }
            context.SaveChanges();

            var exams = new Exam[]
            {
                new Exam{StudentID=1,SubjectID=1050,Grade=8.33,ExamDate=DateTime.Parse("2021-01-21")},
                new Exam{StudentID=3,SubjectID=1045,Grade=9.00,ExamDate=DateTime.Parse("2021-02-10")},
                new Exam{StudentID=1,SubjectID=1045,Grade=5.75,ExamDate=DateTime.Parse("2021-01-23")},
                new Exam{StudentID=2,SubjectID=1050,Grade=8.33,ExamDate=DateTime.Parse("2021-02-02")},
            };
            foreach (Exam e in exams)
            {
                context.Exams.Add(e);
            }
            context.SaveChanges();

            var professors = new Professor[]
            {

                new Professor{ProfessorName="Suciu Ionela",Email="suciuionela@faculty.ro"},
                new Professor{ProfessorName="Dragan Marin",Email="draganmarin@faculty.ro"},
                new Professor{ProfessorName="Bretan Anastasia",Email="bretananastasia@faculty.ro"},};
            foreach (Professor p in professors)
            {
                context.Professors.Add(p);
            }
            context.SaveChanges();

            var courses = new Course[]
           {

                new Course{ProfessorID = professors.Single(c => c.ProfessorName == "Suciu Ionela" ).ID,SubjectID = subjects.Single(i => i.Name == "SIMPA").SubjectID},
                new Course{ProfessorID = professors.Single(c => c.ProfessorName == "Dragan Marin" ).ID,SubjectID = subjects.Single(i => i.Name == "Medii de programare avansate").SubjectID},
                new Course{ProfessorID = professors.Single(c => c.ProfessorName == "Bretan Anastasia" ).ID,SubjectID = subjects.Single(i => i.Name == "Managementul proiectelor").SubjectID},};
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

        }
    }
}
