using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyASPCore.Models;
using MyASPCore.Repository;

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
            var models = _employees.GetAll();
            return View(models);
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}