using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBackendApp.Data;
using MyBackendApp.Models;

namespace MyBackendApp.Repository
{
    public class DepartmentRepository : IDepartment
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteDepartment = await GetById(id);
                _context.Departments.Remove(deleteDepartment);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            var departments = await _context.Departments.ToListAsync();
            return departments;
        }

        public async Task<Department> GetById(int id)
        {
            var department = await _context.Departments.SingleOrDefaultAsync(d => d.DepartmentID == id);
            if (department == null) throw new Exception($"Department Id:{id} not found !");
            return department;
        }

        public async Task<Department> Insert(Department obj)
        {
            try
            {
                _context.Departments.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<Department> Update(Department obj)
        {
            try
            {
                var updateDepartment = await GetById(obj.DepartmentID);
                updateDepartment.DepartmentName = obj.DepartmentName;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}