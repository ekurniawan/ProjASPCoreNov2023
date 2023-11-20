using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBackendApp.DTO;
using MyBackendApp.Models;
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

        [HttpPost]
        public async Task<IActionResult> Post(DepartmentCreateDTO departmentCreateDTO)
        {
            if (departmentCreateDTO == null)
                return BadRequest();

            try
            {
                var insertDepartment = new Department
                {
                    DepartmentName = departmentCreateDTO.DepartmentName
                };
                await _departments.Insert(insertDepartment);

                var departmentDto = new DepartmentDTO
                {
                    DepartmentID = insertDepartment.DepartmentID,
                    DepartmentName = insertDepartment.DepartmentName
                };
                return Ok(departmentDto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DepartmentCreateDTO departmentUpdateDTO)
        {
            try
            {
                var updateDepartment = await _departments.GetById(id);
                updateDepartment.DepartmentName = departmentUpdateDTO.DepartmentName;
                var department = await _departments.Update(updateDepartment);

                var departmentViewDto = new DepartmentDTO
                {
                    DepartmentID = department.DepartmentID,
                    DepartmentName = department.DepartmentName
                };
                return Ok(departmentViewDto);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departments.Delete(id);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}