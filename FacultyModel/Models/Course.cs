using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AvramDaiana_FacultyProject.Models
{
    public class Course
    {
        public int ProfessorID { get; set; }
        public int SubjectID { get; set; }
        public Professor Professor { get; set; }
        public Subject Subject { get; set; }
    }
}
