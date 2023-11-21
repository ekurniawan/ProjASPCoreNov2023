using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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