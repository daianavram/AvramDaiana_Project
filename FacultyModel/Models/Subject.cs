using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvramDaiana_FacultyProject.Models
{
    public class Subject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public ICollection<Exam> Exams { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}
