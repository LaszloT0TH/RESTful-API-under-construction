using EmployeeManagement.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        // EN
        // We are using HttpClient class to call the REST API service.
        // This class is in System.Net.Http namespace.
        // HttpClient is injected into the EmployeeService using dependency injection.
        // We have not registered HttpClient service with the dependency injection container yet.We will do that in just a bit.
        // Pass the REST API endpoint(api/employees) to httpClient.GetJsonAsync method.
        // GE
        // Wir verwenden die HttpClient-Klasse, um den REST-API-Dienst aufzurufen.
        // Diese Klasse befindet sich im System.Net.Http-Namespace.
        // HttpClient wird mithilfe von Abhängigkeitsinjektion in den EmployeeService eingefügt.
        // Wir haben den HttpClient-Dienst noch nicht mit dem Dependency-Injection-Container registriert. Das werden wir in Kürze tun.
        // Übergeben Sie den REST-API-Endpunkt (api/employees) an die Methode httpClient.GetJsonAsync.
        // HU
        // A HttpClient osztályt használjuk a REST API hívásához.
        // Ez az osztály a System.Net.Http névtérben található.
        // A HttpClient függőséginjektálással injektálódik az EmployeeService szolgáltatásba.
        // Még nem regisztráltuk a HttpClient szolgáltatást a függőséginjektálási tárolóval.Ezt egy kicsit meg fogjuk tenni.
        // Adja át a REST API végpontot (api/employees) a httpClient.GetJsonAsync metódusnak.

        // Nuget Package: Microsoft.AspNetCore.Blazor.HttpClient
        // ASP.NET Core default HTTP feature implementations.
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            //return await httpClient.GetFromJsonAsync<Employee[]>("api/employees");

            // ----- or -------------------------------------

            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0&fbclid=IwAR2svwvcl0NIVC0_Hvq4NfhlWsOMIm0f4YUrqnzbZD4oozYO_scT3fcUan0

            //httpClient.DefaultRequestHeaders.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var response = await httpClient.GetAsync("api/employees");

            var responseContent = await response.Content.ReadAsStringAsync();

            var employees = JsonConvert.DeserializeObject<Employee[]>(responseContent);

            return employees;

        }

        public async Task<Employee> GetEmployee(int id)
        {
            //return await httpClient.GetFromJsonAsync<Employee>($"api/employees/{id}");

            // --- or --------------------------------
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0&fbclid=IwAR2svwvcl0NIVC0_Hvq4NfhlWsOMIm0f4YUrqnzbZD4oozYO_scT3fcUan0

            //httpClient.DefaultRequestHeaders.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

            var response = await httpClient.GetAsync("api/employees/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            var employee = JsonConvert.DeserializeObject<Employee>(responseContent);

            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0&fbclid=IwAR2svwvcl0NIVC0_Hvq4NfhlWsOMIm0f4YUrqnzbZD4oozYO_scT3fcUan0
            // EN
            // Serializes the parameter to JSON using .TodoItemSystem.Text.Json
            // Creates an instance of StringContent to package the serialized JSON for sending in the HTTP request's body.
            // Calls PostAsync to send the JSON content to the specified URL.This is a relative URL that gets added to the HttpClient.BaseAddress.
            // Calls EnsureSuccessStatusCode to throw an exception if the response status code doesn't indicate success.
            // GE
            // Serialisiert den Parameter mit .TodoItemSystem.Text.Json in JSON
            // Erstellt eine Instanz von StringContent, um die serialisierte JSON-Datei zum Senden im Hauptteil der HTTP-Anforderung zu verpacken.
            // Ruft PostAsync auf, um den JSON-Inhalt an die angegebene URL zu senden Dies ist eine relative URL, die zu HttpClient.BaseAddress hinzugefügt wird.
            // Ruft CertainSuccessStatusCode auf, um eine Ausnahme auszulösen, wenn der Antwortstatuscode keinen Erfolg anzeigt.
            // HU
            // Szerializálja a paramétert JSON-ra a használatával.TodoItemSystem.Text.Json
            // Létrehoz egy StringContent példányt a szerializált JSON csomagolásához a HTTP-kérés törzsében való küldéshez.
            // Meghívja a PostAsync metódust, hogy elküldje a JSON - tartalmat a megadott URL - címre.Ez egy relatív URL - cím, amely hozzá lesz adva a HttpClient.BaseAddress címhez.
            // Meghívja az EnsureSuccessStatusCode metódust, hogy kivételt dobjon, ha a válasz állapotkódja nem jelzi a sikert.
            var json = JsonConvert.SerializeObject(updatedEmployee);

            var stringContent = new StringContent(json);
            
            var response = await httpClient.PutAsync("api/employees", stringContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            var employee = JsonConvert.DeserializeObject<Employee>(responseContent);

            return employee;
        }

        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0&fbclid=IwAR2svwvcl0NIVC0_Hvq4NfhlWsOMIm0f4YUrqnzbZD4oozYO_scT3fcUan0
            // EN
            // Serializes the parameter to JSON using .TodoItemSystem.Text.Json
            // Creates an instance of StringContent to package the serialized JSON for sending in the HTTP request's body.
            // Calls PostAsync to send the JSON content to the specified URL.This is a relative URL that gets added to the HttpClient.BaseAddress.
            // Calls EnsureSuccessStatusCode to throw an exception if the response status code doesn't indicate success.
            // GE
            // Serialisiert den Parameter mit .TodoItemSystem.Text.Json in JSON
            // Erstellt eine Instanz von StringContent, um die serialisierte JSON-Datei zum Senden im Hauptteil der HTTP-Anforderung zu verpacken.
            // Ruft PostAsync auf, um den JSON-Inhalt an die angegebene URL zu senden Dies ist eine relative URL, die zu HttpClient.BaseAddress hinzugefügt wird.
            // Ruft CertainSuccessStatusCode auf, um eine Ausnahme auszulösen, wenn der Antwortstatuscode keinen Erfolg anzeigt.
            // HU
            // Szerializálja a paramétert JSON-ra a használatával.TodoItemSystem.Text.Json
            // Létrehoz egy StringContent példányt a szerializált JSON csomagolásához a HTTP-kérés törzsében való küldéshez.
            // Meghívja a PostAsync metódust, hogy elküldje a JSON - tartalmat a megadott URL - címre.Ez egy relatív URL - cím, amely hozzá lesz adva a HttpClient.BaseAddress címhez.
            // Meghívja az EnsureSuccessStatusCode metódust, hogy kivételt dobjon, ha a válasz állapotkódja nem jelzi a sikert.
            var json = JsonConvert.SerializeObject(newEmployee);

            var stringContent = new StringContent(json);

            var response = await httpClient.PostAsync("api/employees", stringContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            var employee = JsonConvert.DeserializeObject<Employee>(responseContent);

            return employee;
        }

        public async Task DeleteEmployee(int id)
        {
            await httpClient.DeleteAsync($"api/employees/{id}");
        }
    }
}
