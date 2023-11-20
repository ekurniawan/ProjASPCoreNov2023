using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyASPCore.Data;
using MyASPCore.Models;

namespace MyASPCore.Repository
{
    public class DepartmentEFRepository : IDepartment
    {
        private readonly ApplicationDbContext _context;
        public DepartmentEFRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAll()
        {
            var results = _context.Departments;
            return results;
        }

        public Department GetById(int id)
        {
            var result = _context.Departments.SingleOrDefault(d => d.DepartmentID == id);
            if (result == null)
                throw new Exception($"Department ID: {id} not found !");
            return result;
        }

        public void Insert(Department obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Department obj)
        {
            throw new NotImplementedException();
        }
    }
}