using EmployeeManagement.Models;
using Newtonsoft.Json;

namespace EmployeeManagement.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Department> GetDepartment(int id)
        {
            return await httpClient.GetFromJsonAsync<Department>($"api/departments/{id}");

            // ---- or ----------------------- 
            //var response = await httpClient.GetAsync("api/departments/{id}");

            //var responseContent = await response.Content.ReadAsStringAsync();

            //var department = JsonConvert.DeserializeObject<Department>(responseContent);

            //return department;
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await httpClient.GetFromJsonAsync<Department[]>("api/departments");

            // ---- or ----------------------- 
            //var response = await httpClient.GetAsync("api/departments");

            //var responseContent = await response.Content.ReadAsStringAsync();

            //var departments = JsonConvert.DeserializeObject<Department[]>(responseContent);

            //return departments;
        }
    }
}
