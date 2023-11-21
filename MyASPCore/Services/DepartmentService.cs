using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MyASPCore.Models;

namespace MyASPCore.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            List<Department> departments = new List<Department>();
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5001/api/Departments");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Status Code: {response.StatusCode} - {response.Content.ToString()}");
            string content = await response.Content.ReadAsStringAsync();
            departments = JsonSerializer.Deserialize<List<Department>>(content);
            if (departments == null)
                throw new Exception($"Departments empty !");

            return departments;
        }

        public Task<Department> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> Insert(Department department)
        {
            throw new NotImplementedException();
        }

        public Task<Department> Update(Department department)
        {
            throw new NotImplementedException();
        }
    }
}