using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppWeek01.Data;
using WebAppWeek01.Models;

namespace WebAppWeek01.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly appDbContext _dbContext;

        public EmployeeController(appDbContext dbContext)
        {

            _dbContext = dbContext;

        }
        public IActionResult Index()
        {
            var emp = _dbContext.Employees.Include(p=>p.Position).Include(d=>d.Department);

            return View(emp);
        }
        public IActionResult GetbyID(int? id)
        {
            return View();
        }
        public IActionResult Create() 
        {
            ViewData["PositionId"] = new SelectList(_dbContext.Positions, "Id", "Name");  
            ViewData["DepartmentId"] = new SelectList(_dbContext.Departments, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp) 
        {
            //if(emp.FirstName==emp.FirstName.ToString())
            //{
            //    ModelState.AddModelError("firstname", "Cannot insert the same firstname");//Custom error message validation
            //}else if (emp.LastName == emp.LastName.ToString())
            //{
            //    ModelState.AddModelError("lastname", "Cannot insert the same firstname");
            //}

            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(emp);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var emp=_dbContext.Employees.FirstOrDefault(e=>e.ID== id);
            if(emp==null) return NotFound();

            ViewData["PositionId"] = new SelectList(_dbContext.Positions, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_dbContext.Departments, "Id", "Name");
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(int id,Employee emp)
        {
            var empId=_dbContext.Employees.AsNoTracking()
                .FirstOrDefault(e=>e.ID== id);
            if(empId==null) return NotFound();
            if(ModelState.IsValid)
            {
                _dbContext.Employees.Update(emp);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(emp);

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return View();
        }

    }
}
