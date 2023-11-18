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
            if (TempData["pesan"] != null)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible'><button type='button' class='close' data-dismiss='alert'>&times;</button><strong>Success!</strong>{TempData["pesan"]}</div>";
            }
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

        public IActionResult Edit(int id)
        {
            try
            {
                var editEmployee = _employees.GetById(id);
                EmployeeEditViewModel model = new EmployeeEditViewModel
                {
                    EmployeeID = editEmployee.EmployeeID,
                    FirstName = editEmployee.FirstName,
                    LastName = editEmployee.LastName,
                    Email = editEmployee.Email
                };
                return View(model);
            }
            catch (System.Exception ex)
            {
                return View();
            }
        }


        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(EmployeeEditViewModel employeeEditVM)
        {
            try
            {
                var editEmployee = new Employee
                {
                    EmployeeID = employeeEditVM.EmployeeID,
                    FirstName = employeeEditVM.FirstName,
                    LastName = employeeEditVM.LastName,
                    Email = employeeEditVM.Email
                };
                _employees.Update(editEmployee);

                TempData["pesan"] = $"Data Employee {editEmployee.FirstName} has updated";
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ViewData["error"] = $"<div class='alert alert-danger alert-dismissible'><button type='button' class='close' data-dismiss='alert'>&times;</button><strong>Error!</strong>{ex.Message}</div>";
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var deleteEmployee = _employees.GetById(id);
                EmployeeViewModel model = new EmployeeViewModel
                {
                    EmployeeID = deleteEmployee.EmployeeID,
                    FirstName = deleteEmployee.FirstName,
                    LastName = deleteEmployee.LastName
                };
                return View(model);
            }
            catch (System.Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                _employees.Delete(id);
                TempData["pesan"] = $"Data Employee has deleted";
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ViewData["error"] = $"<div class='alert alert-danger alert-dismissible'><button type='button' class='close' data-dismiss='alert'>&times;</button><strong>Error!</strong>{ex.Message}</div>";
                return View();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel employeeCreateVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                Employee employee = new Employee
                {
                    FirstName = employeeCreateVM.FirstName,
                    LastName = employeeCreateVM.LastName,
                    Email = employeeCreateVM.Email
                };
                _employees.Insert(employee);

                TempData["pesan"] = $"Data Employee {employee.FirstName} has added";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["error"] = $"<div class='alert alert-danger alert-dismissible'><button type='button' class='close' data-dismiss='alert'>&times;</button><strong>Error!</strong>{ex.Message}</div>";
                return View();
            }
        }

    }
}