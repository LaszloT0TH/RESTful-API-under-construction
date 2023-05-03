using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        // EN
        // Finally call IEmployeeService from EmployeeList blazor component.
        // We use[Inject] attribute to inject a service into a Blazor component.We cannot use a constructor for this.
        // In the component OnInitializedAsync method, we call the EmployeeService.GetEmployees method.
        // The data (list of employees) that this method returns is then used to initialise Employees property.
        // The EmployeeList blazor component binds to this Employees property to display the list of employees.
        // GE
        // Schließlich IEmployeeService von der Blazor-Komponente EmployeeList aufrufen.
        // Wir verwenden das Attribut [Inject], um einen Dienst in eine Blazor-Komponente einzufügen. Dafür können wir keinen Konstruktor verwenden.
        // In der Methode OnInitializedAsync der Komponente rufen wir die Methode EmployeeService.GetEmployees auf.
        // Die Daten (Liste der Angestellten), die diese Methode zurückgibt, werden dann verwendet, um die Employees-Eigenschaft zu initialisieren.
        // Die Blazor-Komponente EmployeeList wird an diese Employees-Eigenschaft gebunden, um die Liste der Mitarbeiter anzuzeigen.
        // HU
        // Végül hívja meg az IEmployeeService metódust az EmployeeList blazor összetevőből.
        // Az[Inject] attribútummal injektálunk egy szolgáltatást egy Blazor-összetevőbe.Erre nem használhatunk konstruktort.
        // Az OnInitializedAsync összetevőben meghívjuk az EmployeeService.GetEmployees metódust.
        // A metódus által visszaadott adatok (alkalmazottak listája) ezután az Alkalmazottak tulajdonság inicializálására szolgálnak.
        // Az EmployeeList blazor összetevő ehhez az Alkalmazottak tulajdonsághoz kötődik az alkalmazottak listájának megjelenítéséhez.

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

        // EN
        // The main responsibility of this parent component(EmployeeList) is to retrieve the list of employees by calling the REST API.
        // It also has the ShowFooter boolean property.The Show Footer checkbox on the view is bound to this property.
        // GE
        // Die Hauptaufgabe dieser übergeordneten Komponente (EmployeeList) besteht darin, die Mitarbeiterliste durch Aufrufen der REST-API abzurufen.
        // Es hat auch die boolesche Eigenschaft ShowFooter. Das Kontrollkästchen Fußzeile anzeigen in der Ansicht ist an diese Eigenschaft gebunden.
        // HU
        // Ennek a szülőösszetevőnek (EmployeeList) a fő feladata az alkalmazottak listájának lekérése a REST API.
        // A ShowFooter logikai tulajdonsággal is rendelkezik.A nézet Lábléc megjelenítése jelölőnégyzete ehhez a tulajdonsághoz van kötve.
        public bool ShowFooter { get; set; } = true;

        // EN
        // SelectedEmployeesCount property keeps track of the number of employees selected. This is the property that we increment of decrement depending on the checked state of the checkbox in the child component.
        // EmployeeSelectionChanged is the callback method.This method will be called when the checked state of the checkbox in the child component changes.
        // GE
        // Die Eigenschaft SelectedEmployeesCount verfolgt die Anzahl der ausgewählten Mitarbeiter. Dies ist die Eigenschaft, die wir abhängig vom aktivierten Zustand des Kontrollkästchens in der untergeordneten Komponente erhöhen oder verringern.
        // EmployeeSelectionChanged ist die Callback-Methode. Diese Methode wird aufgerufen, wenn sich der aktivierte Status des Kontrollkästchens in der untergeordneten Komponente ändert.
        // HU
        // A SelectedEmployeesCount tulajdonság nyomon követi a kiválasztott alkalmazottak számát. Ez az a tulajdonság, amelyet a gyermekkomponensben lévő jelölőnégyzet bejelölt állapotától függően növelünk.
        // Az EmployeeSelectionChanged a visszahívási módszer. Ez a metódus akkor kerül meghívásra, ha a gyermekkomponensben lévő jelölőnégyzet bejelölt állapota megváltozik.
        protected int SelectedEmployeesCount { get; set; } = 0;

        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeesCount++;
            }
            else
            {
                SelectedEmployeesCount--;
            }
        }

        // EN
        // A component like EmployeeList component retrieves data from a server asynchronously. Employees property in the base class holds the list of employees. We typically retrieve employees from a database by calling a server side service which is an asynchronous operation. By the time the component is rendered, this asynchronous operation might not have completed. This means Employees property could be null and can result in NullReference exceptions.
        // GE
        // Eine Komponente wie die EmployeeList-Komponente ruft asynchron Daten von einem Server ab. Die Eigenschaft Employees in der Basisklasse enthält die Liste der Mitarbeiter. Normalerweise rufen wir Mitarbeiter aus einer Datenbank ab, indem wir einen serverseitigen Dienst aufrufen, was eine asynchrone Operation ist. Bis die Komponente gerendert wird, ist dieser asynchrone Vorgang möglicherweise noch nicht abgeschlossen. Dies bedeutet, dass die Employees-Eigenschaft null sein und zu NullReference-Ausnahmen führen kann.
        // HU
        //A Blazor komponensek számos életciklus-módszerrel rendelkeznek. Az OnInitializedAsync a leggyakoribb életciklus-módszer. Ezt a módszert felülbíráljuk az alkalmazottak adatainak lekéréséhez.
        // Az olyan összetevők, mint az EmployeeList összetevő, aszinkron módon kérik le az adatokat a kiszolgálóról. Az alaposztály Alkalmazottak tulajdonsága tartalmazza az alkalmazottak listáját. Az alkalmazottakat általában egy szerveroldali szolgáltatás hívásával hívjuk le egy adatbázisból, amely aszinkron művelet. Előfordulhat, hogy az összetevő renderelésének idejére ez az aszinkron művelet nem fejeződött be. Ez azt jelenti, hogy az Alkalmazottak tulajdonság null lehet, és NullReference kivételeket eredményezhet.

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
            // await Task.Run(LoadEmployees);
        }

        protected async Task EmployeeDeleted()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }

        private void LoadEmployees()
        {
            System.Threading.Thread.Sleep(2000);
            // EN
            // Retrieve data from the server and initialize
            // Employees property which the View will bind
            // GE
            // Daten vom Server abrufen und initialisieren
            // Mitarbeitereigenschaft, die die Ansicht bindet
            // HU
            // Adatok lekérése a szerverről és inicializálás
            // Alkalmazotti tulajdonság, amelyet a nézet össze fog kötni
            Employee e1 = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Hastings",
                Email = "David@email.com",
                DateOfBrith = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                DepartmentId = 1, 
                PhotoPath = "images/john.png"
            };

            Employee e2 = new Employee
            {
                EmployeeId = 2,
                FirstName = "Sam",
                LastName = "Galloway",
                Email = "Sam@email.com",
                DateOfBrith = new DateTime(1981, 12, 22),
                Gender = Gender.Male,
                DepartmentId = 2,
                PhotoPath = "images/sam.jpg"
            };

            Employee e3 = new Employee
            {
                EmployeeId = 3,
                FirstName = "Mary",
                LastName = "Smith",
                Email = "mary@email.com",
                DateOfBrith = new DateTime(1979, 11, 11),
                Gender = Gender.Female,
                DepartmentId = 1,
                PhotoPath = "images/mary.png"
            };

            Employee e4 = new Employee
            {
                EmployeeId = 3,
                FirstName = "Sara",
                LastName = "Longway",
                Email = "sara@email.com",
                DateOfBrith = new DateTime(1982, 9, 23),
                Gender = Gender.Female,
                DepartmentId = 3,
                PhotoPath = "images/sara.png"
            };

            Employees = new List<Employee> { e1, e2, e3, e4 };
        }
    }
}
