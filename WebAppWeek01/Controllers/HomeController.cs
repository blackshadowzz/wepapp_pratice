using Microsoft.AspNetCore.Mvc;
using WebAppWeek01.Models;

namespace WebAppWeek01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult view()
        {
            return View();
        }
        public PartialViewResult Index2()
        {
            return PartialView();
        }
        public RedirectResult Index3()
        {
            return Redirect("/home/view");
        }
        public RedirectToActionResult Index4()
        {
            return RedirectToAction("Index");
        }
        public ActionResult Index5()
        {
            ViewBag.Test1 = "Bla ";
            ViewBag.Test2 = new[] { "Bla 1 ", "Bla 2", "Bla 3 " };
            ViewBag.Emp = new Employee { FirstName = "Kosol", ID = 1 };

            return view();
  

        }
        
    }
}
