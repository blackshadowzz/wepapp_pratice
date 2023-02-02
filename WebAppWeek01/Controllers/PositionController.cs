using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using WebAppWeek01.Data;

using Position = WebAppWeek01.Models.Position;

namespace WebAppWeek01.Controllers
{
    public class PositionController : Controller
    {
        private readonly appDbContext _dbContext;

        public PositionController(appDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var pos=_dbContext.Positions;
            return View(pos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Position pos)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Positions.Add(pos);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pos);
        }
    }
}
