﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVCCRUDOperation.Models.Emp;

using System.Data.SqlClient;
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