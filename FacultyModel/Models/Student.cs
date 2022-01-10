using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvramDaiana_FacultyProject.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Faculty { get; set; }
        public int Year { get; set; }
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
