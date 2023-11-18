using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyASPCore.Models;

namespace MyASPCore.Repository
{
    public interface IEmployee : ICrud<Employee>
    {
        IEnumerable<Employee> GetByName();
    }
}