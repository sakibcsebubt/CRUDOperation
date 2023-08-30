using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCCRUDOperation.Models.Emp;

using System.Data.SqlClient;
using System.Reflection;
using static Dapper.SqlMapper;

namespace MVCCRUDOperation.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var EmpList = GetEmpList();
            var EmpList2 = GetEmpList1();
            var sql = "SELECT * FROM EMPLOYEEINFO";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Employee>(sql);
                EmpList = result.ToList();
            }

            EmpolyeeViewModel empolyeeViewModel = new();
            empolyeeViewModel.Employees1 = EmpList;
            empolyeeViewModel.Employees2 = EmpList2;
            return View(empolyeeViewModel);
        }


        public IActionResult Create()
        {
            Employee model = new();
            model.Id = 1;
            model.EmpName = "Test";
            model.Address = "BANGLADESH";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            //model.Address = model.Address == null ? "" : model.Address;

            if (model.Address == null)
            {
                model.Address = "";
            }
            else
            {
                model.Address = model.Address;
            }
            var sql = @"INSERT INTO EMPLOYEEINFO(EmpName,Designation,Address)
	                    OUTPUT Inserted.Id VALUES(@EmpName, @Designation, @Address)";
            var Result = 0;
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Result = await connection.QueryFirstOrDefaultAsync<int>(sql, model);
            }
            if (Result > 0) return View("Success");
            else
                return View();
        }

        public async Task<IActionResult> Delete(long Id)
        {
            try
            {
                var sql = @"DELETE EMPLOYEEINFO WHERE Id = @Id";
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var Result = await connection.ExecuteAsync(sql, new {Id});
                }
            return RedirectToAction("Index");

            }
            catch (Exception ex) 
            {
                //return ex.Message;
                return null;
            }
          
        }




        public List<Employee> GetEmpList()
        {
            //var tes = new Employee();
            //tes.Id = 1;
            //tes.EmpName = "dlskdkfj";
            //tes.Id = 2;
            List<Employee> emplsit = new();
            emplsit.Add(new Employee { Id = 1, EmpName = "Sakib Hasan", Address = "Bangladesh", Designation = "Software Engineer" });
            emplsit.Add(new Employee { Id = 1, EmpName = "Sharif Hasan", Address = "Bangladesh", Designation = "Software Engineer" });
            emplsit.Add(new Employee { Id = 1, EmpName = "Ismail Hossain Nayem", Address = "Bangladesh", Designation = "Software Engineer" });
            emplsit.Add(new Employee { Id = 1, EmpName = "Rayhan Hasan", Address = "Bangladesh", Designation = "Software Engineer" });
            return emplsit;
        }
        public List<Employee> GetEmpList1()
        { 
            List<Employee> emplsit = new();
            emplsit.Add(new Employee { Id = 2, EmpName = "Sakib", Address = "Bangladesh", Designation = "Software Engineer" });
            emplsit.Add(new Employee { Id = 12, EmpName = "Sharif", Address = "Bangladesh", Designation = "Software Engineer" });
            emplsit.Add(new Employee { Id = 12, EmpName = "Ismail", Address = "Bangladesh", Designation = "Software Engineer" });
            emplsit.Add(new Employee { Id = 12, EmpName = "Rayhan", Address = "Bangladesh", Designation = "Software Engineer" });
            return emplsit;
        }
    }
}
