using Microsoft.AspNetCore.Mvc;
using WebAppWeek01.Data;
using WebAppWeek01.Models;

namespace WebAppWeek01.Controllers
{
    public class PersonController : Controller
    {
        private readonly appDbContext _dbContext;

        public PersonController(appDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index( )
        {
            var p = _dbContext.People.ToList();
            //var PersonView = new PersonViewPage
            //{
            //    PerPage = 5,
            //    People = _dbContext.People.OrderBy(d => d.FirstName),
            //    CurrentPage = page
            //};
            //return View(PersonView);
            return View(p);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {

            return View(person);
        }
    }
}
