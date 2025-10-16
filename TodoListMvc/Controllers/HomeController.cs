using Microsoft.AspNetCore.Mvc;

namespace TodoListMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string? userName = Request.Cookies["UserName"];
            if (string.IsNullOrEmpty(userName))
                return View();
            else
                return RedirectToAction("Index", "Tasks");
        }

        [HttpGet]
        public IActionResult CreateName() => View();

        [HttpPost]
        public IActionResult CreateName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var options = new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                };
                Response.Cookies.Append("UserName", name, options);
                return RedirectToAction("Index", "Tasks");
            }

            ModelState.AddModelError("", "Name is required");
            return View();
        }
    }
}
