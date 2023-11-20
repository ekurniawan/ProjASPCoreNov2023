using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.Repository;

namespace MyBackendApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private IDepartment _departments;
        public DepartmentsController(IDepartment departments)
        {
            _departments = departments;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello Web API";
        }
    }
}