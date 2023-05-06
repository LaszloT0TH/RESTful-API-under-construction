using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        // EN
        // Authentication and authorization state data in code in blazor
        // how to obtain authentication and authorization state data in code in blazor
        // Cascading AuthenticationState parameter
        // Cascading AuthenticationState parameter() provides authentication and authorization state data.Task<AuthenticationState>
        // If the user is not authenticated, the request is redirected to the page. login
        // The return url is also passed as the query string parameter to the login page.
        // Upon successful login, the user will be redirected to the page he was trying to access.
        // GE
        // Authentifizierungs- und Autorisierungszustandsdaten im Code in Blazor
        // So erhalten Sie Authentifizierungs- und Autorisierungsstatusdaten im Code in Blazor
        // Kaskadierender AuthenticationState-Parameter
        // Der kaskadierende AuthenticationState-Parameter () stellt Daten zum Authentifizierungs- und Autorisierungsstatus bereit.Task<AuthenticationState>
        // Wenn der Benutzer nicht authentifiziert ist, wird die Anfrage auf die Seite umgeleitet. Anmeldung
        // Die Rückgabe-URL wird auch als Parameter der Abfragezeichenfolge an die Anmeldeseite übergeben.
        // Nach erfolgreicher Anmeldung wird der Benutzer auf die Seite umgeleitet, auf die er zugreifen wollte.
        // HU
        // Szerep- vagy házirend-alapú engedélyezés
        // hogyan szerezheti be a hitelesítési és engedélyezési állapotadatokat a blazor kódjában.
        // Cascading AuthenticationState paraméter
        // A Cascading AuthenticationState paraméter() hitelesítési és engedélyezési állapotadatokat biztosít.Task<AuthenticationState>
        // Ha a felhasználó nincs hitelesítve, a rendszer átirányítja a kérést az oldalra.login
        // A visszatérési URL-cím lekérdezési sztring paraméterként is át lesz adva a bejelentkezési oldalnak.
        // Sikeres bejelentkezés után a felhasználó átirányításra kerül arra az oldalra, amelyhez megpróbált hozzáférni.
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        // EN
        // Check if authenticated user satisfies a specific policy
        //Task<AuthenticationState> can be combined with , to check if a specific aothorization policy is satisfied.IAuthorizationService
        // GE
        // Überprüfen Sie, ob der authentifizierte Benutzer eine bestimmte Richtlinie erfüllt
        // Task<AuthenticationState> kann mit kombiniert werden, um zu prüfen, ob eine bestimmte Autorisierungsrichtlinie erfüllt ist.IAuthorizationService
        // HU
        // Ellenőrizze, hogy a hitelesített felhasználó megfelel-e egy adott szabályzatnak
        //Task<AuthenticationState> kombinálható a -val, hogy ellenőrizze, hogy egy adott aothorizációs irányelv teljesül-e.IAuthorizationService
        //public class EditEmployeeBase : ComponentBase
        //        {
        //            [CascadingParameter]
        //            private Task<AuthenticationState> authenticationStateTask { get; set; }

        //            [Inject]
        //            private IAuthorizationService AuthorizationService { get; set; }

        //            protected async override Task OnInitializedAsync()
        //            {
        //                var user = (await authenticationStateTask).User;

        //                if ((await AuthorizationService.AuthorizeAsync(user, "admin-policy"))
        //                .Succeeded)
        //                {
        //                    // Execute code specific to admin-policy
        //                }
        //            }
        //        }
        // - - - - -  END COMMENT  - - - - - - - - - - - - 






        // EN
        // retrieves the employee details by ID.
        // GE
        // Ruft die Mitarbeiterdetails nach ID ab.
        // HU
        // lekéri az alkalmazott adatait azonosító alapján.
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public string PageHeader { get; set; }

        // EN
        // Used to store the Rest API call, property contains the employee data we want to edit.
        // GE
        // Wird verwendet, um den Rest-API-Aufruf zu speichern, Eigenschaft enthält die Mitarbeiterdaten, die wir bearbeiten möchten.
        // HU
        // A Rest API hívás tárolására használjuk. A tulajdonság tartalmazza a szerkeszteni kívánt alkalmazotti adatokat.
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
            var authenticationState = await authenticationStateTask;

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/editEmployee/{Id}");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }
            // EN
            // Check if authenticated user is in a specific role
            // GE
            // Prüfen, ob der authentifizierte Benutzer eine bestimmte Rolle hat
            // HU
            // Ellenőrizze, hogy a hitelesített felhasználó egy adott szerepkörben van-e
            //if (authenticationState.User.IsInRole("Administrator"))
            //{
            //   Execute Admin logic
            //}


            // old code
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