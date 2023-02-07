using Microsoft.AspNetCore.Mvc;
using WebAppWeek01.Data;
using WebAppWeek01.Models;

namespace WebAppWeek01.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly appDbContext _dbContext;
        public DepartmentController(appDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Department> emplist = _dbContext.Departments;
            return View(emplist);
        }
        public IActionResult CreatePartial()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Department dep)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Departments.Add(dep);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(dep);

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
               

        }
    }
}
