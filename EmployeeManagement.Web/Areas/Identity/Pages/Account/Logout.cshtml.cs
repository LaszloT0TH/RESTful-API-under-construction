// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Web.Areas.Identity.Pages.Account
{
    // EN
    // ASP.NET Core Identity logout page
    // ASP.NET Core Identity logout page is in Areas/Identity/Pages/Account/Logout.cshtml.
    // ASP.NET Core Identity scaffolder provided the implementation of the Logout page.
    // To signout the user, method is used.This method removes the authentication cookie.SignInManager.SignOutAsync()
    // SignOutAsync() method is called on the form POST.When we click the navigation link, a GET request is issued to the logout page.Logout
    // OnGet() method in Logout page is empty.Nothing happens.It just displays the static text - You have successfully logged out of the application.
    // To delete the autentication cookie and log out the user a POST request must be issued to the page.Logout
    // You can do this by clicking the link on the top right hand corner of the Logout page.Logout
    // The and the submit button are in file.logoutFormlogoutPages/Shared/_LoginPartial.cshtml
    // Submit logout form automatically
    // Include the following JavaScript code to automatically submit the logout form.We are using an IIFE here.IIFE stands for Immediately Invoked Function Expression. It is also called as a Self-Executing Anonymous Function.In simple terms it is a JavaScript function that runs as soon as it is defined.
    // Logout.cshtml
    // <script>
    // (() => {
    //   document.getElementById('logoutForm').submit();
    // })()
    // </ script >
    // GE
    // ASP.NET Core Identity-Abmeldeseite
    // ASP.NET Core Identity-Abmeldeseite befindet sich in Areas/Identity/Pages/Account/Logout.cshtml.
    // ASP.NET Core Identity Scaffolder stellte die Implementierung der Abmeldeseite bereit.
    // Um den Benutzer abzumelden, wird die Methode verwendet. Diese Methode entfernt das Authentifizierungscookie.SignInManager.SignOutAsync()
    // SignOutAsync()-Methode wird im Formular POST aufgerufen. Wenn wir auf den Navigationslink klicken, wird eine GET-Anforderung an die Abmeldeseite ausgegeben. Abmelden
    // Die Methode OnGet() auf der Abmeldeseite ist leer. Es passiert nichts. Es wird nur der statische Text angezeigt - Sie haben sich erfolgreich von der Anwendung abgemeldet.
    // Um das Authentifizierungs-Cookie zu löschen und den Benutzer abzumelden, muss eine POST-Anforderung an die Seite gesendet werden. Logout
    // Sie können dies tun, indem Sie auf den Link in der oberen rechten Ecke der Abmeldeseite klicken.Abmelden
    // Die und die Senden-Schaltfläche befinden sich in file.logoutFormlogoutPages/Shared/_LoginPartial.cshtml
    // Abmeldeformular automatisch absenden
    // Fügen Sie den folgenden JavaScript-Code ein, um das Abmeldeformular automatisch zu senden.Wir verwenden hier ein IIFE.IIFE steht für Instant Invoked Function Expression.Sie wird auch als selbstausführende anonyme Funktion bezeichnet.Einfach ausgedrückt handelt es sich um eine JavaScript-Funktion, die ausgeführt wird, sobald sie definiert ist. 
    // Logout.cshtml
    // <script>
    // (() => {
    //   document.getElementById('logoutForm').submit();
    // })()
    // </ script >
    // HU
    // ASP.NET Alapvető identitás kijelentkezési oldala
    // ASP.NET alapvető identitáskijelentkezési oldal a Areas/Identity/Pages/Account/Logout.cshtml fájlban található.
    // ASP.NET Core Identity állvány biztosította a Kijelentkezés oldal megvalósítását.
    // A felhasználó kijelentkezéséhez a metódust használjuk. Ez a módszer eltávolítja a hitelesítési cookie-t.SignInManager.SignOutAsync()
    // A SignOutAsync() metódus meghívása a POST űrlapon történik.Amikor a navigációs hivatkozásra kattintunk, egy GET kérés érkezik a kijelentkezési oldalra.Kijelentkezés
    // Az OnGet() metódus a Kijelentkezési oldalon üres. Nem történik semmi. Csak a statikus szöveget jeleníti meg - Sikeresen kijelentkezett az alkalmazásból.
    // A hitelesítési cookie törléséhez és a felhasználó kijelentkezéséhez POST kérést kell küldeni az oldalnak.Kijelentkezés
    // Ezt a Kijelentkezési oldal jobb felső sarkában található hivatkozásra kattintva teheti meg.Kijelentkezés
    // A és a küldés gomb a file.logoutFormlogoutPages/Shared/_LoginPartial.cshtml fájlban található
    // Kijelentkezési űrlap automatikus elküldése
    // Adja meg a következő JavaScript-kódot a kijelentkezési űrlap automatikus elküldéséhez.Itt IIFE-t használunk. Az IIFE az Immediate Invoked Function Expression rövidítése. Önvégrehajtó névtelen függvénynek is nevezik.Egyszerűen fogalmazva, ez egy JavaScript függvény, amely azonnal fut, amint definiálják.
    // Logout.cshtml
    // <script>
    // (() => {
    //   document.getElementById('logoutForm').submit();
    // })()
    // </ script >
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.

                // EN
                // Redirect the user to application root
                // If you do not want to stay on the ASP.NET Core Identity Logout page, you can redirect the user to the application root URL by modifying the code in method of Logout.cshtml.
                // GE
                // Den Benutzer zum Anwendungsstamm umleiten
                // Wenn Sie nicht auf der ASP.NET Core Identity-Abmeldeseite bleiben möchten, können Sie den Benutzer zur Stamm-URL der Anwendung umleiten, indem Sie den Code in der Methode von Logout.cshtml ändern.
                // HU
                // Átirányítás az alkalmazás gyökerére
                // Ha nem szeretne a ASP.NET Core Identity Logout oldalon maradni, átirányíthatja a felhasználót az alkalmazás gyökér URL-címére a kód módosításával a Logout.cshtml metódusban.

                //return RedirectToPage();
                return LocalRedirect("~/");
            }
        }
    }
}
