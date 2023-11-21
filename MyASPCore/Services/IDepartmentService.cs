using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyASPCore.Models;

namespace MyASPCore.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(int id);
        Task<Department> Insert(Department department);
        Task<Department> Update(Department department);
        Task Delete(int id);
    }
}