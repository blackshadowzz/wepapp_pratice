using Microsoft.AspNetCore.Mvc;

namespace WebAppWeek01.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
