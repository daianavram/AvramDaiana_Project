using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvramDaiana_FacultyProject.Models
{
    public class Exam
    {
        public int ExamID { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public double Grade { get; set; }
        public DateTime ExamDate { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
