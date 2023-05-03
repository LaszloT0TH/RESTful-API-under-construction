using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Pages
{
    public class DisplayEmployeeBase : ComponentBase
    {
        [Parameter]
        public Employee Employee { get; set; }

        [Parameter]
        public bool ShowFooter { get; set; }

        // EN
        // The parent component must be notified, when the checkbox checked status changes in the child component. For this the child component exposes an event. The parent component assigns a callback method to the child component's event. In Blazor, to expose an event we use EventCallback.
        // GE
        // Die Parent-Komponente muss benachrichtigt werden, wenn sich der Checkbox-Checked-Status in der Child-Komponente ändert. Dazu stellt die untergeordnete Komponente ein Ereignis bereit. Die übergeordnete Komponente weist dem Ereignis der untergeordneten Komponente eine Callback-Methode zu. In Blazor verwenden wir EventCallback, um ein Ereignis anzuzeigen.
        // HU
        // A szülőösszetevőt értesíteni kell, ha a jelölőnégyzet bejelölte az állapotváltozásokat a gyermekösszetevőben. Ehhez a gyermekösszetevő elérhetővé tesz egy eseményt. A szülőösszetevő visszahívási metódust rendel a gyermekösszetevő eseményéhez. A Blazorban egy esemény felfedéséhez az EventCallback-et használjuk.

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        // EN
        // OnEmployeeSelection is the event this child component is exposing. It is of type EventCallback.
        // EventCallback supports generics which we use to pass event data (also commonly called event payload).
        // In our example, we are passing bool. A value of true or false depending on whether the checkbox is checked or not.
        // Checkbox binds to IsSelected boolean property.
        // CheckBoxChanged method is the event hanlder.It stores the checked state of the checkbox in IsSelected property.It also raises OnEmployeeSelection custom event. 
        // Checkbox checked state is passed as the event payload to OnEmployeeSelection event.
        // GE
        // OnEmployeeSelection ist das Ereignis, das diese untergeordnete Komponente verfügbar macht. Es ist vom Typ EventCallback.
        // EventCallback unterstützt Generika, die wir zum Übergeben von Ereignisdaten verwenden (auch allgemein als Ereignisnutzlast bezeichnet).
        // In unserem Beispiel übergeben wir bool. Ein Wert von wahr oder falsch, je nachdem, ob das Kontrollkästchen aktiviert ist oder nicht.
        // Kontrollkästchen wird an die boolesche Eigenschaft IsSelected gebunden.
        // CheckBoxChanged-Methode ist der Event-Handler. Sie speichert den aktivierten Zustand des Kontrollkästchens in der IsSelected-Eigenschaft. Sie löst auch das benutzerdefinierte OnEmployeeSelection-Ereignis aus.
        // Der aktivierte Zustand des Kontrollkästchens wird als Ereignisnutzlast an das Ereignis OnEmployeeSelection übergeben.
        // HU
        // Az OnEmployeeSelection az az esemény, amelyet ez a gyermekösszetevő felfed. EventCallback típusú.
        // Az EventCallback támogatja az általános adatokat, amelyeket az eseményadatok(más néven esemény hasznos terhelése) továbbítására használunk.
        // Példánkban átadjuk a boolt.Igaz vagy hamis érték attól függően, hogy a jelölőnégyzet be van-e jelölve vagy sem.
        // A jelölőnégyzet az IsSelected logikai tulajdonsághoz kötődik.
        // A CheckBoxChanged metódus a hanlder esemény. A jelölőnégyzet bejelölt állapotát az IsSelected tulajdonságban tárolja. Emellett egyéni eseményt is felvet OnEmployeeSelection .
        // A jelölőnégyzet bejelölve állapota az esemény hasznos adataként lesz átadva az OnEmployeeSelection eseménynek.
        protected bool IsSelected { get; set; }

        [Parameter]
        public EventCallback<bool> OnEmployeeSelection { get; set; }

        // EN
        // Child component to parent component communication
        // Child component(DisplayEmployee Component) should notify the parent component(EmployeeList Component) that a record is deleted so the parent component can remove the respective employee card from the list of employees.For this the child component exposes an event. The parent component assigns a callback method to the child component's event. In Blazor, to expose an event we use .EventCallback
        // Child Component Class (DisplayEmployeeBase.cs)
        // OnEmployeeDeleted is the custom event. 
        // We use to create a custom event. We discussed this in Part 28 of Blazor tutorial.EventCallback
        // Delete_Click event handler deletes the employee record and raises the custom event - .OnEmployeeDeleted
        // GE
        // Kommunikation von untergeordneter Komponente zu übergeordneter Komponente
        // Untergeordnete Komponente (DisplayEmployee-Komponente) soll der übergeordneten Komponente (EmployeeList-Komponente) mitteilen, dass ein Datensatz gelöscht wurde, damit die übergeordnete Komponente die entsprechende Mitarbeiterkarte aus der Liste der Mitarbeiter entfernen kann. Dazu stellt die untergeordnete Komponente ein Ereignis bereit. Die übergeordnete Komponente weist dem Ereignis der untergeordneten Komponente eine Callback-Methode zu. In Blazor verwenden wir .EventCallback, um ein Ereignis anzuzeigen
        // Untergeordnete Komponentenklasse (DisplayEmployeeBase.cs)
        // OnEmployeeDeleted ist das benutzerdefinierte Ereignis.
        // Wir verwenden, um ein benutzerdefiniertes Ereignis zu erstellen. Wir haben dies in Teil 28 von Blazor tutorial.EventCallback besprochen
        // Delete_Click-Ereignishandler löscht den Mitarbeiterdatensatz und löst das benutzerdefinierte Ereignis aus - .OnEmployeeDeleted
        // HU
        // Gyermekösszetevő és szülőösszetevő közötti kommunikáció
        // A gyermekösszetevőnek(DisplayEmployee összetevő) értesítenie kell a szülőösszetevőt(EmployeeList Component) egy rekord törléséről, hogy a szülőösszetevő eltávolíthassa a megfelelő alkalmazotti kártyát az alkalmazottak listájáról.Ehhez a gyermekösszetevő elérhetővé tesz egy eseményt.A szülőösszetevő visszahívási metódust rendel a gyermekösszetevő eseményéhez.A Blazorban egy esemény felfedéséhez használjuk.EventCallback
        // Gyermekösszetevő-osztály (DisplayEmployeeBase.cs)
        // OnEmployeeDeleted az egyéni esemény.
        // Egyéni esemény létrehozására használjuk. Ezt a Blazor oktatóanyag 28. részében tárgyaltuk.EventCallback
        // Delete_Click Az eseménykezelő törli az alkalmazotti rekordot, és létrehozza az egyéni eseményt - .OnEmployeeDeleted
        [Parameter]
        public EventCallback<int> OnEmployeeDeleted { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(Employee.EmployeeId);
            await OnEmployeeDeleted.InvokeAsync(Employee.EmployeeId);
            //NavigationManager.NavigateTo("/", true);
        }

        protected async Task CheckBoxChanged(ChangeEventArgs e)
        {
            IsSelected = (bool)e.Value;
            await OnEmployeeSelection.InvokeAsync(IsSelected);
        }
    }
}