using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGrades.Data;
using StudentGrades.Models;

namespace StudentGrades.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly AppDbContext _db;
        public SubjectsController(AppDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Subjects.OrderBy(s => s.SubjectName).ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectName")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _db.Subjects.Add(subject);
                await _db.SaveChangesAsync();
                TempData["ToastMessage"] = "Subject added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _db.Subjects.FindAsync(id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectKey,SubjectName")] Subject subject)
        {
            if (id != subject.SubjectKey) return BadRequest();
            if (ModelState.IsValid)
            {
                _db.Subjects.Update(subject);
                await _db.SaveChangesAsync();
                TempData["ToastMessage"] = "Subject updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _db.Subjects.FindAsync(id);
            if (subject == null) return NotFound();
            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _db.Subjects.Include(s => s.Students).FirstOrDefaultAsync(s => s.SubjectKey == id);
            if (subject != null)
            {
                if (subject.Students != null && subject.Students.Any())
                {
                    TempData["ErrorMessage"] = "Cannot delete this subject. Students are assigned to it.";
                    return RedirectToAction(nameof(Index));
                }
                _db.Subjects.Remove(subject);
                await _db.SaveChangesAsync();
                TempData["ToastMessage"] = "Subject deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
