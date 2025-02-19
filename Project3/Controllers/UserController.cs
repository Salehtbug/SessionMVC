using Microsoft.AspNetCore.Mvc;

namespace Project3.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HandleLogin(string email, string password)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Password = HttpContext.Session.GetString("Password");

            if (email == Email && password == Password)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["MSG2"] = "Password or Email not valid";
            return RedirectToAction("Login");


        }

        [HttpPost]
        public IActionResult HandleRegister(string name, string email, string password, string repassword)
        {

            HttpContext.Session.SetString("Name", name);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("Password", password);
            HttpContext.Session.SetString("RePassword", repassword);


            if (password != repassword)
            {
                TempData["MSG"] = "Password doesn't match";
                return RedirectToAction("Register");

            }

            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.Password = HttpContext.Session.GetString("Password");



            TempData["Name"] = HttpContext.Session.GetString("Name");
            ViewData["Name"] = HttpContext.Session.GetString("Name");



            return View();
        }

    }
}
