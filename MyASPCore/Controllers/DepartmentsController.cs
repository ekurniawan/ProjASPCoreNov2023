using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyASPCore.Models;
using MyASPCore.Services;
using MyASPCore.ViewModels;

namespace MyASPCore.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IDepartmentService _departmentService;
        public DepartmentsController(ILogger<DepartmentsController> logger, IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<DepartmentViewModel> departmentViewModels = new List<DepartmentViewModel>();
                var models = await _departmentService.GetAll();
                foreach (var dept in models)
                {
                    departmentViewModels.Add(new DepartmentViewModel
                    {
                        DepartmentID = dept.DepartmentID,
                        DepartmentName = dept.DepartmentName
                    });
                }
                return View(departmentViewModels);
            }
            catch (System.Exception ex)
            {
                ViewData["error"] = ex.Message;
                return Content(ex.Message);
            }

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateViewModel departmentCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                Department department = new Department
                {
                    DepartmentName = departmentCreateViewModel.DepartmentName
                };
                await _departmentService.Insert(department);

                TempData["pesan"] = $"Data Department {department.DepartmentName} has added";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["error"] = $"<div class='alert alert-danger alert-dismissible'><button type='button' class='close' data-dismiss='alert'>&times;</button><strong>Error!</strong>{ex.Message}</div>";
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var editDepartment = await _departmentService.GetById(id);
                DepartmentViewModel model = new DepartmentViewModel
                {
                    DepartmentID = editDepartment.DepartmentID,
                    DepartmentName = editDepartment.DepartmentName
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
        public async Task<IActionResult> EditPost(DepartmentViewModel departmentViewModel)
        {
            try
            {
                var editDepartment = new Department
                {
                    DepartmentID = departmentViewModel.DepartmentID,
                    DepartmentName = departmentViewModel.DepartmentName
                };
                await _departmentService.Update(editDepartment);

                TempData["pesan"] = $"Data Department {editDepartment.DepartmentName} has updated";
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ViewData["error"] = $"<div class='alert alert-danger alert-dismissible'><button type='button' class='close' data-dismiss='alert'>&times;</button><strong>Error!</strong>{ex.Message}</div>";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}