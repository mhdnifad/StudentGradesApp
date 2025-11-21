using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentGrades.Data;
using StudentGrades.Models;

namespace StudentGrades.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext _db;
        public StudentsController(AppDbContext db)
        {
            _db = db;
        }

        // GET: Index with search and filter
        public async Task<IActionResult> Index(string? search, string? filter)
        {
            var query = _db.Students.Include(s => s.Subject).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(s => s.StudentName.Contains(search));

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = filter switch
                {
                    "PASS" => query.Where(s => s.Grade >= 75),
                    "FAIL" => query.Where(s => s.Grade < 75),
                    _ => query
                };
            }

            // Pass data to view
            ViewBag.Search = search;
            ViewBag.Filter = filter;

            // Filter dropdown using SelectList (for Razor 8 safe)
            ViewBag.FilterList = new SelectList(new[]
            {
                new { Value = "", Text = "All" },
                new { Value = "PASS", Text = "PASS" },
                new { Value = "FAIL", Text = "FAIL" }
            }, "Value", "Text", filter);

            return View(await query.OrderBy(s => s.StudentName).ToListAsync());
        }

        // GET: Create Student
        public IActionResult Create()
        {
            ViewBag.SubjectList = new SelectList(
                _db.Subjects.OrderBy(s => s.SubjectName),
                "SubjectKey",
                "SubjectName"
            );
            return View();
        }

        // POST: Create Student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentName,SubjectKey,Grade")] Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                await _db.SaveChangesAsync();
                TempData["ToastMessage"] = "Student added successfully!";
                return RedirectToAction(nameof(Index));
            }

            // Repopulate dropdown if validation fails
            ViewBag.SubjectList = new SelectList(
                _db.Subjects.OrderBy(s => s.SubjectName),
                "SubjectKey",
                "SubjectName",
                student.SubjectKey
            );

            return View(student);
        }

        // GET: Edit Student
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student == null) return NotFound();

            ViewBag.SubjectList = new SelectList(
                _db.Subjects.OrderBy(s => s.SubjectName),
                "SubjectKey",
                "SubjectName",
                student.SubjectKey
            );

            return View(student);
        }

        // POST: Edit Student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentKey,StudentName,SubjectKey,Grade")] Student student)
        {
            if (id != student.StudentKey) return BadRequest();

            if (ModelState.IsValid)
            {
                _db.Students.Update(student);
                await _db.SaveChangesAsync();
                TempData["ToastMessage"] = "Student updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SubjectList = new SelectList(
                _db.Subjects.OrderBy(s => s.SubjectName),
                "SubjectKey",
                "SubjectName",
                student.SubjectKey
            );

            return View(student);
        }

        // GET: Delete Student
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students
                                   .Include(s => s.Subject)
                                   .FirstOrDefaultAsync(s => s.StudentKey == id);

            if (student == null) return NotFound();
            return View(student);
        }

        // POST: Delete Student
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _db.Students.FindAsync(id);
            if (student != null)
            {
                _db.Students.Remove(student);
                await _db.SaveChangesAsync();
                TempData["ToastMessage"] = "Student deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
