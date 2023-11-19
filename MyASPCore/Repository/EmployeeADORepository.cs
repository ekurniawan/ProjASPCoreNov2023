using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
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
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"delete Employees where EmployeeID=@ EmployeeID";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Number} - {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
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
            Employee employee = new Employee();
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                string strSql = @"select * from Employees 
                                  where EmployeeID=@EmployeeID";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    employee.EmployeeID = Convert.ToInt32(dr["EmployeeID"]);
                    employee.FirstName = dr["FirstName"].ToString();
                    employee.LastName = dr["LastName"].ToString();
                    employee.Email = dr["Email"].ToString();
                }
                else
                {
                    throw new Exception($"Data Employee {id} tidak ditemukan");
                }
            }
            return employee;
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
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Number} - {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
        }

        public void Update(Employee obj)
        {
            try
            {
                var result = GetById(obj.EmployeeID);

                using (SqlConnection conn = new SqlConnection(GetConnStr()))
                {
                    string strSql = @"update Employees set FirstName=@FirstName,LastName=@LastName,@Email=@Email 
                                  where EmployeeID=@EmployeeID";
                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@EmployeeID", obj.EmployeeID);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sqlEx)
                    {
                        throw new Exception($"{sqlEx.Number} - {sqlEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"{ex.Message}");
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }


        }

        public void InsertBatch()
        {
            StringBuilder sb = new StringBuilder();
            using (SqlConnection conn = new SqlConnection(GetConnStr()))
            {
                for (int i = 1; i <= 10; i++)
                {
                    sb.Append($"insert into Employees(FirstName,LastName,Email) values('FirstName{i}','LastName{i}','Email{i}@gmail.com');");
                }

                SqlCommand cmd = new SqlCommand(sb.ToString(), conn);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
            }
        }

        public void ProcessPayroll()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    Employee emp1 = new Employee
                    {
                        FirstName = "Employee1",
                        LastName = "Employee1",
                        Email = "employee1@gmail.com"
                    };
                    Insert(emp1);

                    Employee empUpdate1 = new Employee
                    {
                        FirstName = "EmployeeUpdate1",
                        LastName = "EmployeeYpdate1",
                        Email = "employee1@gmail.com",
                        EmployeeID = 1
                    };
                    Update(empUpdate1);

                    Employee emp2 = new Employee
                    {
                        FirstName = "Employee2",
                        LastName = "Employee2",
                        Email = "employee2@gmail.com"
                    };
                    Insert(emp2);

                    InsertBatch();

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public IEnumerable<EmployeeWithDepartment> GetEmployeeWithDepartment()
        {
            throw new NotImplementedException();
        }
    }
}