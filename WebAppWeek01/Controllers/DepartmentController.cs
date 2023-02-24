using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAppWeek01.Data;
using WebAppWeek01.Models;

namespace WebAppWeek01.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly appDbContext _dbContext;
        private const string conString= "Server=(localdb)\\MSSQLLocalDB;Database=WebWeek01;Trusted_connection=true;TrustServerCertificate=true";
        public DepartmentController(appDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Department> emplist = _dbContext.Departments;
            var dep = GetDep();
            return View(dep);
        }
        public IActionResult CreatePartial()
        {
            return View();
        }

        private List<Department> GetDep()
        {
            List<Department> list = new List<Department>();
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using(var cmd=new SqlCommand("Select * From Departments", conn))
                {
                    DataTable dt = new DataTable();
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    list=(from dr in dt.AsEnumerable()
                          select new Department
                          {
                              Id=dr.Field<int>("Id"),
                              Name=dr.Field<string>("Name"),
                              Description=dr.Field<string>("Description")
                          }).ToList();

                }

            }
            return list;
        }

        [HttpPost]
        public IActionResult Create(Department dep)
        {
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        _dbContext.Departments.Add(dep);
            //        _dbContext.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //    return View(dep);

            //}
            //catch(Exception ex)
            //{
            //    return BadRequest(ex);
            //}
            if(ModelState.IsValid)
            {
                CreateDeparment(dep);
                return RedirectToAction("Index");
            }
               return View(dep);

        }
        private bool CreateDeparment(Department dep)
        {
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"INSERT INTO Departments(Name,Description)
                                                VALUES(@name,@dep)", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", dep.Name);
                    cmd.Parameters.AddWithValue("@dep", dep.Description);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
