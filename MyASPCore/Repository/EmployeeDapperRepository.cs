using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyASPCore.Models;
using Dapper;

namespace MyASPCore.Repository
{
    public class EmployeeDapperRepository : IEmployee
    {
        private readonly IConfiguration _config;
        public EmployeeDapperRepository(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnStr()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"delete Employees where EmployeeID=@ EmployeeID";
                var param = new { EmployeeID = id };
                try
                {
                    conn.Execute(strSql, param);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Employees order by FirstName asc";
                var employees = conn.Query<Employee>(strSql);
                return employees;
            }
        }

        public Employee GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Employees where EmployeeID=@EmployeeID";
                var param = new { EmployeeID = id };
                var result = conn.QuerySingleOrDefault<Employee>(strSql, param);
                return result;
            }
        }

        public IEnumerable<Employee> GetByName()
        {
            throw new NotImplementedException();
        }

        public void Insert(Employee obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"insert into Employees(FirstName,LastName,Email) values(@FirstName,@LastName,@Email)";
                var param = new { FirstName = obj.FirstName, LastName = obj.LastName, Email = obj.Email };
                try
                {
                    conn.Execute(strSql, param);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Message}");
                }
            }
        }

        public void ProcessPayroll()
        {
            throw new NotImplementedException();
        }

        public void Update(Employee obj)
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"update Employees set FirstName=@FirstName,LastName=@LastName,@Email=@Email 
                                  where EmployeeID=@EmployeeID";
                var param = new
                {
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    Email = obj.Email,
                    EmployeeID = obj.EmployeeID
                };

                try
                {
                    conn.Execute(strSql, param);
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Message}");
                }
            }
        }

        public IEnumerable<EmployeeWithDepartment> GetEmployeeWithDepartment()
        {
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from EmployeeWithDepartmentView";
                var results = conn.Query<EmployeeWithDepartment>(strSql);
                return results;
            }
        }
    }
}