using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvramDaiana_FacultyProject.Data;
using AvramDaiana_FacultyProject.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace AvramDaiana_FacultyProject.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly FacultyContext _context;
        private string _baseUrl = "http://localhost:59644/api/Subjects";

        public SubjectsController(FacultyContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var subjects = JsonConvert.DeserializeObject<List<Subject>>(await
               response.Content.
                ReadAsStringAsync());
                return View(subjects);
            }
            return NotFound();
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var subject = JsonConvert.DeserializeObject<Subject>(
                await response.Content.ReadAsStringAsync());
                return View(subject);
            }
            return NotFound();
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectID,Name,Credits")] Subject subject)
        {
            if (!ModelState.IsValid) return View(subject);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(subject);
                var response = await client.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: { ex.Message}");
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var subject = JsonConvert.DeserializeObject<Subject>(
                await response.Content.ReadAsStringAsync());
                return View(subject);
            }
            return new NotFoundResult();
        }
        
        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectID,Name,Credits")] Subject subject)
        {
            if (!ModelState.IsValid) return View(subject);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(subject);
            var response = await client.PutAsync($"{_baseUrl}/{subject.SubjectID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var subject = JsonConvert.DeserializeObject<Subject>(await
               response.Content.ReadAsStringAsync());
                return View(subject);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("SubjectID")] Subject subject)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete,
               $"{_baseUrl}/{subject.SubjectID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(subject),
               Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record:{ ex.Message}");
            }
            return View(subject);
        }
    }
}
