using AvramDaiana_FacultyProject.Data;
using AvramDaiana_FacultyProject.Models;
using AvramDaiana_FacultyProject.Models.FacultyViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AvramDaiana_FacultyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly FacultyContext _context;
        public HomeController(FacultyContext context)
        {
            _context = context;
        }

        public IActionResult Chat()
        {
            return View();
        }

        private readonly ILogger<HomeController> _logger;

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
         from subject in _context.Exams
         group subject by subject.Subject.Name into dateGroup
         select new OrderGroup()
         {
                ExamName = dateGroup.Key,
                StudentsCount = dateGroup.Count()
         };

            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
