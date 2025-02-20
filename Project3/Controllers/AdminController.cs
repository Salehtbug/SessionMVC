using Microsoft.AspNetCore.Mvc;

namespace Project3.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
