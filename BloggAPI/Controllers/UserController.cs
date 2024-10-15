using Microsoft.AspNetCore.Mvc;

namespace BloggAPI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
