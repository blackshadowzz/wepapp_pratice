using Microsoft.AspNetCore.Mvc;

namespace WebAppWeek01.Controllers
{
    public class MovieArtController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
