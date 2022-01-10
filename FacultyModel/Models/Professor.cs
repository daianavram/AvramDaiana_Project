using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvramDaiana_FacultyProject.Models
{
    public class Professor
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Professor Name")]
        [StringLength(50)]
        public string ProfessorName { get; set; }

        [StringLength(70)]
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
