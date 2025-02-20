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


        public IActionResult Signout()
        {
            foreach (var item in HttpContext.Session.Keys)
                HttpContext.Session.Remove(item);

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult SaveUser(string Name, string Email, string Password, string Re_Password)
        {

            HttpContext.Session.SetString("Name", Name);
            HttpContext.Session.SetString("Email", Email);
            HttpContext.Session.SetString("Password", Password);
            HttpContext.Session.SetString("Re_Password", Re_Password);


            return RedirectToAction("Login", "User");
        }


        [HttpPost]
        public IActionResult LoginCheckUser()
        {
            string isChecked = Request.Form["RememberMe"].ToString();

            if (isChecked == "true")
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                HttpContext.Response.Cookies.Append("Email", Request.Form["EmailLogin"], options);

                HttpContext.Response.Cookies.Append("Password", Request.Form["PasswordLogin"], options);

            }

            if (HttpContext.Session.GetString("Email") == Request.Form["EmailLogin"] &&
                HttpContext.Session.GetString("Email") != null &&
                HttpContext.Session.GetString("Password") == Request.Form["PasswordLogin"] &&
                HttpContext.Session.GetString("Password") != null)
            {
                HttpContext.Session.SetString("signData", "Signed");

                return RedirectToAction("Index", "Home");
            }
            else if (Request.Form["EmailLogin"] == "Admin@gmail.com" && Request.Form["PasswordLogin"] == "123321")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
                ViewBag.MSGERROR = "Your Password or Email is incorrect..";

            return RedirectToAction("Login", "User");

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
