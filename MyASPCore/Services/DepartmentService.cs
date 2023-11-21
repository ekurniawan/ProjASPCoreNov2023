using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task Delete(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
              new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                  "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImVyaWNrQGdtYWlsLmNvbSIsInJvbGUiOlsiYWRtaW4iLCJmaW5hbmNlIl0sIm5iZiI6MTcwMDU0OTMxNSwiZXhwIjoxNzAwNjM1NzE1LCJpYXQiOjE3MDA1NDkzMTV9.DtaTq3vOmZS9QoJiDraQHutGdnZqZECnQLgDQ0D-IIk");
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"http://localhost:5001/api/Departments/{id}");
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Status Code: {response.StatusCode} - {response.Content.ToString()}");
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            List<Department> departments = new List<Department>();
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImVyaWNrQGdtYWlsLmNvbSIsInJvbGUiOlsiYWRtaW4iLCJmaW5hbmNlIl0sIm5iZiI6MTcwMDU0OTMxNSwiZXhwIjoxNzAwNjM1NzE1LCJpYXQiOjE3MDA1NDkzMTV9.DtaTq3vOmZS9QoJiDraQHutGdnZqZECnQLgDQ0D-IIk");
            HttpResponseMessage response = await _httpClient.GetAsync("http://localhost:5001/api/Departments");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Status Code: {response.StatusCode} - {response.Content.ToString()}");

            string content = await response.Content.ReadAsStringAsync();
            departments = JsonSerializer.Deserialize<List<Department>>(content);
            if (departments == null)
                throw new Exception($"Departments empty !");

            return departments;
        }

        public async Task<Department> GetById(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImVyaWNrQGdtYWlsLmNvbSIsInJvbGUiOlsiYWRtaW4iLCJmaW5hbmNlIl0sIm5iZiI6MTcwMDU0OTMxNSwiZXhwIjoxNzAwNjM1NzE1LCJpYXQiOjE3MDA1NDkzMTV9.DtaTq3vOmZS9QoJiDraQHutGdnZqZECnQLgDQ0D-IIk");
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5001/api/Departments/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Status Code: {response.StatusCode} - {response.Content.ToString()}");
            string content = await response.Content.ReadAsStringAsync();
            var department = JsonSerializer.Deserialize<Department>(content);
            return department;
        }

        public async Task<Department> Insert(Department department)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImVyaWNrQGdtYWlsLmNvbSIsInJvbGUiOlsiYWRtaW4iLCJmaW5hbmNlIl0sIm5iZiI6MTcwMDU0OTMxNSwiZXhwIjoxNzAwNjM1NzE1LCJpYXQiOjE3MDA1NDkzMTV9.DtaTq3vOmZS9QoJiDraQHutGdnZqZECnQLgDQ0D-IIk");
            string jsonObject = JsonSerializer.Serialize(department);
            StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PostAsync("http://localhost:5001/api/Departments", content);
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Status Code: {response.StatusCode} - {response.Content.ToString()}");

                string outputContent = await response.Content.ReadAsStringAsync();
                var outputDepartment = JsonSerializer.Deserialize<Department>(outputContent);

                return outputDepartment;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Department> Update(Department department)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                    "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImVyaWNrQGdtYWlsLmNvbSIsInJvbGUiOlsiYWRtaW4iLCJmaW5hbmNlIl0sIm5iZiI6MTcwMDU0OTMxNSwiZXhwIjoxNzAwNjM1NzE1LCJpYXQiOjE3MDA1NDkzMTV9.DtaTq3vOmZS9QoJiDraQHutGdnZqZECnQLgDQ0D-IIk");
            string jsonObject = JsonSerializer.Serialize(department);
            StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response =
                    await _httpClient.PutAsync($"http://localhost:5001/api/Departments/{department.DepartmentID}", content);
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Status Code: {response.StatusCode} - {response.Content.ToString()}");

                string outputContent = await response.Content.ReadAsStringAsync();
                var outputDepartment = JsonSerializer.Deserialize<Department>(outputContent);

                return outputDepartment;

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}