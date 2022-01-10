using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvramDaiana_FacultyProject.Models.FacultyViewModels
{
    public class ProfessorIndexData
    {
        public IEnumerable<Professor> Professors { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<Exam> Exams { get; set; }
    }
}
