using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorProject.Components
{
    public class ConfirmBase : ComponentBase
    {
        protected bool ShowConfirmation { get; set; }

        [Parameter]
        public string ConfirmationTitle { get; set; } = "Confirm Delete";

        [Parameter]
        public string ConfirmationMessage { get; set; } = "Are you sure you want to delete";

        public void Show()
        {
            ShowConfirmation = true;

            // EN
            // ComponentBase Class Method
            // Notifies the component that its state has changed. When applicable, this will
            // cause the component to be re-rendered.
            // GE
            // ComponentBase-Klassenmethode
            // Benachrichtigt die Komponente, dass sich ihr Zustand geändert hat. Gegebenenfalls wird dies
            // Bewirken, dass die Komponente neu gerendert wird.
            // HU
            // ComponentBase osztály módszer
            // Értesíti az összetevőt, hogy az állapota megváltozott. Adott esetben ez lesz
            // az összetevő újbóli megjelenítését okozza.
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        protected async Task OnConfirmationChange(bool value)
        {
            ShowConfirmation = false;

            // EN
            // Invokes the delegate associated with this binding and dispatches an event notification to the appropriate component.
            // GE
            // Ruft den Delegaten auf, der dieser Bindung zugeordnet ist, und sendet eine Ereignisbenachrichtigung an die entsprechende Komponente.
            // HU
            // Meghívja az ehhez az összerendeléshez társított delegált, és eseményértesítést küld a megfelelő összetevőnek.
            await ConfirmationChanged.InvokeAsync(value);
        }
    }
}
