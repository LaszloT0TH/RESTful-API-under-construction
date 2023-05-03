using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public string PageHeader { get; set; }

        // EN
        // Used to store the Rest API call
        // GE
        // Wird verwendet, um den Rest-API-Aufruf zu speichern
        // HU
        // A Rest API hívás tárolására használjuk
        private Employee Employee { get; set; } = new Employee();

        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        //public string DepartmentId { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            //Employee = await EmployeeService.GetEmployee(int.Parse(Id));

            int.TryParse(Id, out int employeeId);

            if (employeeId != 0)
            {
                PageHeader = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                PageHeader = "Create Employee";
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBrith = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg"
                };
            }

            Departments = (await DepartmentService.GetDepartments()).ToList();

            //DepartmentId = Employee.DepartmentId.ToString();


            // NuGet Package: AutoMapper.Extensions.Microsoft.DependencyInjection
            // EN
            // It replaces this code.
            // GE
            // Er ersetzt diesen Code.
            // HU
            // Ezt a kódot helyettesíti.
            //EditEmployeeModel.EmployeeId = Employee.EmployeeId;
            //EditEmployeeModel.FirstName = Employee.FirstName;
            //EditEmployeeModel.LastName = Employee.LastName;
            //EditEmployeeModel.Email = Employee.Email;
            //EditEmployeeModel.ConfirmEmail = Employee.Email;
            //EditEmployeeModel.DateOfBrith = Employee.DateOfBrith;
            //EditEmployeeModel.Gender = Employee.Gender;
            //EditEmployeeModel.PhotoPath = Employee.PhotoPath;
            //EditEmployeeModel.DepartmentId = Employee.DepartmentId;
            //EditEmployeeModel.Department = Employee.Department;

            Mapper.Map(Employee, EditEmployeeModel);
        }

        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);

            //var result = await EmployeeService.UpdateEmployee(Employee);
            //if (result != null)
            //{
            //    NavigationManager.NavigateTo("/");
            //}

            Employee result = null;

            if (Employee.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                result = await EmployeeService.CreateEmployee(Employee);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected BlazorProject.Components.ConfirmBase DeleteConfirmation { get; set; }

        protected void Delete_Click()
        {
            DeleteConfirmation.Show();
        }

        protected async Task ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await EmployeeService.DeleteEmployee(Employee.EmployeeId);
                NavigationManager.NavigateTo("/");
            }
        }

        //protected async Task Delete_Click()
        //{
        //    await EmployeeService.DeleteEmployee(Employee.EmployeeId);
        //    NavigationManager.NavigateTo("/");
        //}
    }
}