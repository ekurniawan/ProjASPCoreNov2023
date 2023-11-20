using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.DTO;
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
        public async Task<IEnumerable<DepartmentDTO>> Get()
        {
            List<DepartmentDTO> departmentsDto = new List<DepartmentDTO>();
            var departments = await _departments.GetAll();
            foreach (var dept in departments)
            {
                departmentsDto.Add(new DepartmentDTO
                {
                    DepartmentID = dept.DepartmentID,
                    DepartmentName = dept.DepartmentName
                });
            }

            return departmentsDto;
        }

        [HttpGet("{id}")]
        public async Task<DepartmentDTO> Get(int id)
        {
            var department = await _departments.GetById(id);
            DepartmentDTO departmentDto = new DepartmentDTO
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName
            };

            return departmentDto;
        }
    }
}