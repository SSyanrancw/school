using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace School.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly ModelSchool _context;

        [HttpGet]
        public IActionResult Index()
        {
            var estudiantes = _context.People
            .Where(r => r.EnrollmentDate != null)
            .Include(r => r.StudentGrades)
            .ToList();

            return View(estudiantes);
        }

        public IActionResult Ficha(int id)
        {
            var estudiante = _context.People
                .Where(r => r.EnrollmentDate != null)
                .Include(r => r.StudentGrades)
                .Where(r => r.PersonID == id)
                .FirstOrDefault();

            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ficha(int id, Person person)
        {
            if(id != person.PersonID) return NotFound();

            if(ModelState.IsValid)
            {
                _context.Update(person);
                _context.SaveChanges();

                return RedirectToAction("index");
            } else {
                return View(person);
            }
        }

        public EstudiantesController(ModelSchool context)
        {
            _context = context;
        }
    }
}