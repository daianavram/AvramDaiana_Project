using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AvramDaiana_FacultyProject.Models.FacultyViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public string ExamName { get; set; }
        public DateTime? ExamDate{ get; set; }
        public int StudentsCount { get; set; }
    }
}
