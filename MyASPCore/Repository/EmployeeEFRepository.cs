using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyASPCore.Data;
using MyASPCore.Models;

namespace MyASPCore.Repository
{
    public class EmployeeEFRepository : IEmployee
    {
        private readonly ApplicationDbContext _context;
        public EmployeeEFRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            try
            {
                var deleteEmployee = GetById(id);
                _context.Employees.Remove(deleteEmployee);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            var results = _context.Employees.Include(nameof(Department));
            /*var results = from e in _context.Employees
                          select e;*/
            return results;
        }

        public Employee GetById(int id)
        {
            //var result = _context.Employees.SingleOrDefault(e => e.EmployeeID == id);
            var result = (from e in _context.Employees
                          where e.EmployeeID == id
                          select e).SingleOrDefault();
            if (result == null)
                throw new Exception($"Employee ID: {id} not found !");
            return result;
        }

        public IEnumerable<Employee> GetByName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeWithDepartment> GetEmployeeWithDepartment()
        {
            throw new NotImplementedException();
        }

        public void Insert(Employee obj)
        {
            try
            {
                _context.Employees.Add(obj);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ProcessPayroll()
        {
            throw new NotImplementedException();
        }

        public void Update(Employee obj)
        {
            try
            {
                var updateEmployee = GetById(obj.EmployeeID);
                updateEmployee.FirstName = obj.FirstName;
                updateEmployee.LastName = obj.LastName;
                updateEmployee.Email = obj.Email;

                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}