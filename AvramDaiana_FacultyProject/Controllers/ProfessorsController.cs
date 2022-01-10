using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvramDaiana_FacultyProject.Data;
using AvramDaiana_FacultyProject.Models;
using AvramDaiana_FacultyProject.Models.FacultyViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AvramDaiana_FacultyProject.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly FacultyContext _context;

        [HttpGet]
        [Route("/Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }

        public ProfessorsController(FacultyContext context)
        {
            _context = context;
        }

        // GET: Professors
        public async Task<IActionResult> Index(int? id, int? subjectID)
        {
            var viewModel = new ProfessorIndexData();
            viewModel.Professors = await _context.Professors
            .Include(i => i.Courses)
            .ThenInclude(i => i.Subject)
            .ThenInclude(i => i.Exams)
            .ThenInclude(i => i.Student)
            .AsNoTracking()
            .OrderBy(i => i.ProfessorName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["ID"] = id.Value;
                Professor professor = viewModel.Professors.Where(
                i => i.ID == id.Value).Single();
                viewModel.Subjects = professor.Courses.Select(s => s.Subject);
            }
            if (subjectID != null)
            {
                ViewData["SubjectID"] = subjectID.Value;
                viewModel.Exams = viewModel.Subjects.Where(
                x => x.SubjectID == subjectID).Single().Exams;
            }
            return View(viewModel);
        }

        // GET: Professors/Details/5
        [Authorize(Policy = "OnlyProfessors")]
        //[Authorize(Policy = "OnlyStudents")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professors/Create
        [Authorize(Policy = "OnlyAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlyAdmin")]
        public async Task<IActionResult> Create([Bind("ID,ProfessorName,Email")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        // GET: Professors/Edit/5
        [Authorize(Policy = "OnlyAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var professor = await _context.Professors
            .Include(i => i.Courses).ThenInclude(i => i.Subject)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }
           PopulateCourseData(professor);
            return View(professor);
        }

        private void PopulateCourseData(Professor professor)
        {
            var allSubjects = _context.Subjects;
            var professorsSubjects = new HashSet<int>(professor.Courses.Select(c => c.SubjectID));
            var viewModel = new List<CourseData>();
            foreach (var subject in allSubjects)
            {
                viewModel.Add(new CourseData
                {
                    SubjectID = subject.SubjectID,
                    CourseName = subject.Name,
                    IsMandatory = professorsSubjects.Contains(subject.SubjectID)
                });
            }
            ViewData["Subjects"] = viewModel;
        }


        // POST: Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlyProfessor")]
        public async Task<IActionResult> Edit(int? id, string[] selectedSubjects)
        {
            if (id == null)
            {
                return NotFound();
            }
            var professorToUpdate = await _context.Professors
            .Include(i => i.Courses)
            .ThenInclude(i => i.Subject)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Professor>(
            professorToUpdate,
            "",
            i => i.ProfessorName, i => i.Email))
            {
                UpdateCourses(selectedSubjects, professorToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateCourses(selectedSubjects, professorToUpdate);
            PopulateCourseData(professorToUpdate);
            return View(professorToUpdate);
        }

        [Authorize(Policy = "OnlyAdmin")]
        private void UpdateCourses(string[] selectedSubjects, Professor professorToUpdate)
        {
            if (selectedSubjects == null)
            {
                professorToUpdate.Courses = new List<Course>();
                return;
            }
            var selectedSubjectsHS = new HashSet<string>(selectedSubjects);
            var courses = new HashSet<int>
            (professorToUpdate.Courses.Select(c => c.Subject.SubjectID));
            foreach (var subject in _context.Subjects)
            {
                if (selectedSubjectsHS.Contains(subject.SubjectID.ToString()))
                {
                    if (!courses.Contains(subject.SubjectID))
                    {
                        professorToUpdate.Courses.Add(new Course
                        {
                            ProfessorID =
                       professorToUpdate.ID,
                            SubjectID = subject.SubjectID
                        });
                    }
                }
                else
                {
                    if (courses.Contains(subject.SubjectID))
                    {
                        Course subjectToRemove = professorToUpdate.Courses.FirstOrDefault(i
                       => i.SubjectID == subject.SubjectID);
                        _context.Remove(subjectToRemove);
                    }
                }
            }
        }



        // GET: Professors/Delete/5
        [Authorize(Policy = "OnlyAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professors.FindAsync(id);
            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professors.Any(e => e.ID == id);
        }
    }
}
