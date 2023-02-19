using Microsoft.AspNetCore.Mvc;

namespace WebAppWeek01.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
