using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyASPCore.Models;

namespace MyASPCore.Repository
{
    public class InMemoryEmployeeRepository : IEmployee
    {

        private List<Employee> employees;
        public InMemoryEmployeeRepository()
        {
            employees = new List<Employee>{
                new Employee{EmployeeID=1,FirstName="Scott",LastName="Guthrie",Email="scott@gmail.com"},
                new Employee{EmployeeID=2,FirstName="Anders",LastName="Helsjberg",Email="anders@gmail.com"},
                new Employee{EmployeeID=3,FirstName="Scott",LastName="Guthrie",Email="scott@gmail.com"},
                new Employee{EmployeeID=4,FirstName="Scott",LastName="Hanselman",Email="hanselman@gmail.com"}
            };
        }
        public void Delete(int id)
        {
            try
            {
                var delEmployee = GetById(id);
                employees.Remove(delEmployee);
            }
            catch (System.Exception ex)
            {
                throw new Exception($"{ex.Message} - {ex.InnerException.Message}");
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            return employees.OrderBy(e => e.FirstName);
        }

        public Employee GetById(int id)
        {
            var result = employees.SingleOrDefault(e => e.EmployeeID == id);
            if (result == null)
                throw new Exception($"Data Employee {id} not found...");
            return result;
        }

        public IEnumerable<Employee> GetByName()
        {
            throw new NotImplementedException();
        }

        public void Insert(Employee obj)
        {
            employees.Add(obj);
        }

        public void Update(Employee obj)
        {
            try
            {
                var empUpdate = GetById(obj.EmployeeID);
                empUpdate.FirstName = obj.FirstName;
                empUpdate.LastName = obj.LastName;
                empUpdate.Email = obj.Email;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}