using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyASPCore.Models;
using MyASPCore.Repository;
using MyASPCore.ViewModels;

namespace MyASPCore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployee _employees;
        public EmployeesController(IEmployee employees)
        {
            _employees = employees;
        }

        public IActionResult Index()
        {
            var employees = _employees.GetAll();
            List<EmployeeViewModel> models = new List<EmployeeViewModel>();
            foreach (var emp in employees)
            {
                models.Add(new EmployeeViewModel
                {
                    EmployeeID = emp.EmployeeID,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName
                });
            }
            return View(models);
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}