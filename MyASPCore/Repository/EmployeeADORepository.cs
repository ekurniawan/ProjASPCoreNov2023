using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MyASPCore.Models;

namespace MyASPCore.Repository
{
    public class EmployeeADORepository : IEmployee
    {
        private readonly IConfiguration _config;
        public EmployeeADORepository(IConfiguration config)
        {
            _config = config;
        }

        private string GetConnStr()
        {
            return _config.GetConnectionString("DefaultConnection");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            List<Employee> lstEmployee = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Employees order by FirstName asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstEmployee.Add(new Employee
                        {
                            EmployeeID = Convert.ToInt32(dr["EmployeeID"]),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            Email = dr["Email"].ToString()
                        });
                    }
                }
            }
            return lstEmployee;
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetByName()
        {
            throw new NotImplementedException();
        }

        public void Insert(Employee obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee obj)
        {
            throw new NotImplementedException();
        }
    }
}