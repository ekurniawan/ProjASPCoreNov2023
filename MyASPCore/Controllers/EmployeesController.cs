using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyASPCore.Models;

namespace MyASPCore.Controllers
{
    public class EmployeesController : Controller
    {
        public EmployeesController()
        {
        }

        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>{
                new Employee{EmployeeID=1,FirstName="Scott",LastName="Guthrie",Email="scott@gmail.com"},
                new Employee{EmployeeID=2,FirstName="Anders",LastName="Helsjberg",Email="anders@gmail.com"}
            };

            var lstNama = new string[] { "erick", "scott", "budi" };
            //return Content("Hello ASP Core 8");
            ViewData["username"] = "ekurniawan";
            ViewData["nama"] = lstNama;
            ViewBag.Address = "jogja";
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}