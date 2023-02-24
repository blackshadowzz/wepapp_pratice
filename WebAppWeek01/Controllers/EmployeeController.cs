using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAppWeek01.Data;
using WebAppWeek01.Models;

namespace WebAppWeek01.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly appDbContext _dbContext;
        private const string conString = "Server=(localdb)\\MSSQLLocalDB;Database=WebWeek01;Trusted_connection=true;TrustServerCertificate=true";
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeController(appDbContext dbContext,IWebHostEnvironment webHost)
        {

            _dbContext = dbContext;
            webHostEnvironment = webHost;

        }
        public IActionResult Index()
        {
            //var emp = _dbContext.Employees.Include(p=>p.Position).Include(d=>d.Department);
            var emp = GetEmp();
            return View(GetEmp());
        }
        public IActionResult Index2()
        {
            var emp = _dbContext.Employees;
            ViewBag.dataSource = emp;
            return View();
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
                //string fileName = FileUpload(emp);
                //emp.Photo = fileName;
                //_dbContext.Attach(emp);
                //_dbContext.Entry(emp).State=EntityState.Added;
                ////_dbContext.Employees.Add(emp);
                //_dbContext.SaveChanges();
                //return RedirectToAction("Index");
                CreateEmp(emp);
                return RedirectToAction("Index");
            }
            return View(emp);
        }
        private string FileUpload(Employee employee)
        {
            string fileName = null;
            if (employee.FrontImage != null)
            {
                string fileFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                fileName=Guid.NewGuid().ToString()+"_"+ employee.FrontImage.FileName;
                string filePath=Path.Combine(fileFolder, fileName);
                using (var fileStream= new FileStream(filePath, FileMode.Create))
                {
                    employee.FrontImage.CopyTo(fileStream);
                }
            }

            return fileName;
        }
        public IActionResult Edit(int? id)
        {
            //if (id == null || id == 0)
            //{
            //    return NotFound();
            //}
            //var emp=_dbContext.Employees.FirstOrDefault(e=>e.ID== id);
            //if(emp==null) return NotFound();

            var em=GetEmpById(id);

            ViewData["PositionId"] = new SelectList(_dbContext.Positions, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_dbContext.Departments, "Id", "Name");
            return View(em);
        }

        [HttpPost]
        public IActionResult Edit(int id,Employee emp)
        {
            //var empId=_dbContext.Employees.AsNoTracking()
            //    .FirstOrDefault(e=>e.ID== id);
            //if(empId==null) return NotFound();
            //if(ModelState.IsValid)
            //{
            //    _dbContext.Employees.Update(emp);
            //    _dbContext.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            if(UpdateEmp(id, emp)) return RedirectToAction("Index");

            return View(emp);

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (DeleteEmp(id)) return RedirectToAction("Index");
            return BadRequest();

            //return View();
        }


        //Using ADO.Net SqlConnection
        //Method Get Department
        private List<Employee> GetEmp()
        {
            List<Employee> list = new List<Employee>();
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("Select * From Employees", conn))
                {
                    DataTable dt = new DataTable();
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    list = (from dr in dt.AsEnumerable()
                            select new Employee
                            {
                                ID = dr.Field<int>("Id"),
                                FirstName = dr.Field<string>("FirstName"),
                                LastName = dr.Field<string>("LastName"),
                                DOB=dr.Field<DateTime?>("DOB"),
                                Gender = dr.Field<string>("Gender"),
                                Address = dr.Field<string>("Address"),
                                City = dr.Field<string>("City"),
                                Province= dr.Field<string>("Province"),
                                Country = dr.Field<string>("Province"),
                                Phone = dr.Field<string>("Phone"),
                                Email = dr.Field<string>("Email"),
                              
                                PositionId = dr.Field<int?>("PositionId"),
                                DepartmentId = dr.Field<int?>("DepartmentId"),
                            }).ToList();

                }

            }
            return list;
        }
        //Method Create Employee
        private bool CreateEmp(Employee emp)
        {
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"INSERT INTO Employees(FirstName,LastName,DOB,Gender,Address,City,
                                                                        Province,Country,Phone,Email,PositionId,DepartmentId)
                                                VALUES(@FirstName,@LastName,@DOB,@Gender,@Address,@City,@Province,
                                                        @Country,@Phone,@Email,@PositionId,@DepartmentId)", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@DOB", emp.DOB);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@City", emp.City);
                    cmd.Parameters.AddWithValue("@Province", emp.Province);
                    cmd.Parameters.AddWithValue("@Country", emp.Country);
                    cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@PositionId", emp.PositionId);
                    cmd.Parameters.AddWithValue("@DepartmentId", emp.DepartmentId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //Method Get Employee by Id
        private Employee GetEmpById(int? id)
        {
            Employee emp = new();
            emp = GetEmp().Where(d => d.ID == id)
                                        .FirstOrDefault();
            return emp;

        }
        //Method Update Employee 
        private bool UpdateEmp(int id,Employee emp)
        {
            var em=GetEmpById(id);
            if(em == null) return false;
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"Update Employees Set FirstName=@FirstName,
                                                                       LastName=@LastName,DOB=@DOB,
                                                                       Gender=@Gender,Address=@Address,
                                                                       City=@City,
                                                                        Province=@Province,Country=@Country,Phone=@Phone,Email=@Email,PositionId=@PositionId,
                                                                        DepartmentId=@DepartmentId Where ID=@Id
                                                                        ", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id",id);
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@DOB", emp.DOB);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@City", emp.City);
                    cmd.Parameters.AddWithValue("@Province", emp.Province);
                    cmd.Parameters.AddWithValue("@Country", emp.Country);
                    cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@PositionId", emp.PositionId);
                    cmd.Parameters.AddWithValue("@DepartmentId", emp.DepartmentId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //Method delete employee
        private bool DeleteEmp(int id)
        {
            var dp = GetEmpById(id);
            if (dp == null) return false;
            using (var conn = new SqlConnection(conString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"DELETE FROM Employees WHERE ID=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
