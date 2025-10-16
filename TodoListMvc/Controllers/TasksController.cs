using Microsoft.AspNetCore.Mvc;
using TodoListMvc.DataAccess;
using TodoListMvc.Models;

namespace TodoListMvc.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TasksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var userName = Request.Cookies["UserName"];
            ViewBag.UserName = userName;

            var tasks = _db.TodoItemes.ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", fileName);

                    using (var stream = System.IO.File.Create(path))
                    {
                        file.CopyTo(stream);
                    }

                    model.File = "/uploads/" + fileName;
                }

                _db.TodoItemes.Add(model);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _db.TodoItemes.Find(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(TodoItem model, IFormFile? file)
        {
            var existing = _db.TodoItemes.Find(model.Id);
            if (existing == null) return NotFound();

            existing.Title = model.Title;
            existing.Description = model.Description;
            existing.Deadline = model.Deadline;

            if (file != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", fileName);

                using (var stream = System.IO.File.Create(path))
                {
                    file.CopyTo(stream);
                }

                existing.File = "/uploads/" + fileName;
            }

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _db.TodoItemes.Find(id);
            if (task == null) return NotFound();

            _db.TodoItemes.Remove(task);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
