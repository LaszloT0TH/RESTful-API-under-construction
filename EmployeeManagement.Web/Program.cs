using EmployeeManagement.Web.Services;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Web.Data;

namespace Company.WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            #region Comment
            // EN
            // User Registration
            // To be able to login a user needs a user account with our system. So the user fills a registration form with their preferred username and password and posts the form to the server. The server then hashes the password and stores stores it in the database. Hashing prevents password theft. Even if an attacker cracks and gains access to your system, they won't be able to steal passwords because they are hashed. Hashing is different from encryption.
            // Encryption vs Hashing
            // Encryption is reversible, i.e what is encrypted can also be decrypted.Hashing, is one - way.It is irreversible.Hashing scrambles plain text to produce a unique message digest. If implemented using a strong algorithm, there is no way to reverse the hashing process to reveal the original password. 
            // An attacker can hash random passwords and then compare the hashes to crack the password.
            // When a random salt is added to the hashing process, the generated hash will not be the same, even if the plain text passwords are.
            // User Login
            // The registered username and password can then be used on the login form.The login form is posted to the server. The server looks up the username in the database. Hashes the supplied password, and compares it to the already hashed password in the database. If they match, then the system knows, the user is who he claims to be, otherwise access is denied by sending HTTP status code 401.
            // If the supplied username and password matches, the server creates an access token which uniquely identifies the user's session. This access token is stored in the database and is also attached to the response cookie. This cookie is then returned to the client. The user is now logged in.
            // Subsequent Requests
            // On every subsequent request, the browser automatically sends the cookie to the server. The server reads the access token from the cookie and checks it against the one in the database associated with that user. If they match, access is granted.
            // Once the user logs out of the application, both, the authentication cookie and the access token in the database are deleted.
            // Cookie authentication or token authentication
            // The following is our Blazor application architecture. We have a Blazor web application and a Web API. Blazor web application calls WEB API. Client <-> Blazor Server <-> Restful Service <-> Database
            //Depending on how you want your application to scale you may have both blazor web app and web api deployed on same server or different servers. Depending on the demand, if you want to be able to independently scale up and down these 2 applications, then you may have to deploy them on different servers.
            //If they are deployed on different servers, we cannot use the same cookie authentication to authenticate both the Blazor web application and web api.This is because a cookie created by one domain cannot be accessed by another domain.
            //Although it is possible to share cookies between sub-domains, it is a standard practice to use cookie based authentication for web applications and token based authentication for web apis.
            //In our upcoming videos, we will implement cookie authentication to protect our blazor web application and token authentication to protect our Web API.
            // GE
            // Benutzer Registration
            // Um sich anmelden zu können, benötigt ein Benutzer ein Benutzerkonto bei unserem System. Der Benutzer füllt also ein Registrierungsformular mit seinem bevorzugten Benutzernamen und Passwort aus und sendet das Formular an den Server. Der Server hasht dann das Passwort und speichert es in der Datenbank. Hashing verhindert Passwortdiebstahl. Selbst wenn ein Angreifer Ihr System knackt und sich Zugriff verschafft, kann er keine Passwörter stehlen, da sie gehasht sind. Hashing unterscheidet sich von Verschlüsselung.
            // Verschlüsselung vs. Hashing
            // Die Verschlüsselung ist reversibel, d. h. was verschlüsselt ist, kann auch entschlüsselt werden. Hashing ist einseitig. Es ist irreversibel. Wenn es mit einem starken Algorithmus implementiert wird, gibt es keine Möglichkeit, den Hash-Prozess umzukehren, um das ursprüngliche Passwort zu enthüllen.
            // Ein Angreifer kann zufällige Passwörter hashen und dann die Hashes vergleichen, um das Passwort zu knacken.
            // Wenn dem Hash-Prozess ein zufälliges Salt hinzugefügt wird, ist der generierte Hash nicht derselbe, selbst wenn die Klartext-Passwörter es sind.
            // Benutzer-Anmeldung
            // Der registrierte Benutzername und das Passwort können dann im Anmeldeformular verwendet werden. Das Anmeldeformular wird an den Server gesendet. Der Server sucht den Benutzernamen in der Datenbank. Hasht das bereitgestellte Passwort und vergleicht es mit dem bereits gehashten Passwort in der Datenbank. Wenn sie übereinstimmen, weiß das System, dass der Benutzer derjenige ist, für den er sich ausgibt, andernfalls wird der Zugriff durch Senden des HTTP-Statuscodes 401 verweigert.
            // Wenn der angegebene Benutzername und das Passwort übereinstimmen, erstellt der Server ein Zugriffstoken, das die Sitzung des Benutzers eindeutig identifiziert. Dieses Zugriffstoken wird in der Datenbank gespeichert und auch an das Antwort-Cookie angehängt. Dieses Cookie wird dann an den Client zurückgegeben. Der Benutzer ist jetzt angemeldet.
            // Nachfolgende Anfragen
            // Bei jeder weiteren Anfrage sendet der Browser das Cookie automatisch an den Server. Der Server liest das Zugriffstoken aus dem Cookie und vergleicht es mit dem Token in der Datenbank, das diesem Benutzer zugeordnet ist. Bei Übereinstimmung wird der Zugriff gewährt.
            // Sobald sich der Benutzer von der Anwendung abmeldet, werden sowohl das Authentifizierungs-Cookie als auch das Zugriffstoken in der Datenbank gelöscht.
            // Cookie-Authentifizierung oder Token-Authentifizierung
            // Das Folgende ist unsere Blazor-Anwendungsarchitektur. Wir haben eine Blazor-Webanwendung und eine Web-API. Die Blazor-Webanwendung ruft die WEB-API auf. Client <-> Blazor Server <-> Restful Service <-> Datenbank
            //Je nachdem, wie Sie Ihre Anwendung skalieren möchten, können Sie sowohl die Blazor-Web-App als auch die Web-API auf demselben Server oder auf verschiedenen Servern bereitstellen. Wenn Sie diese beiden Anwendungen unabhängig voneinander hoch- und herunterskalieren möchten, müssen Sie sie je nach Bedarf möglicherweise auf verschiedenen Servern bereitstellen.
            //Wenn sie auf verschiedenen Servern bereitgestellt werden, können wir nicht dieselbe Cookie-Authentifizierung verwenden, um sowohl die Blazor-Webanwendung als auch die Web-API zu authentifizieren. Dies liegt daran, dass auf ein von einer Domäne erstelltes Cookie von einer anderen Domäne nicht zugegriffen werden kann.
            //Obwohl es möglich ist, Cookies zwischen Subdomänen zu teilen, ist es eine Standardpraxis, die Cookie-basierte Authentifizierung für Webanwendungen und die Token-basierte Authentifizierung für Web-APIs zu verwenden.
            //In unseren kommenden Videos werden wir die Cookie-Authentifizierung zum Schutz unserer Blazor-Webanwendung und die Token-Authentifizierung zum Schutz unserer Web-API implementieren.
            // HU
            // Felhasználó regisztráció
            // Ahhoz, hogy a felhasználó bejelentkezhessen, felhasználói fiókra van szüksége a rendszerünkben. Így a felhasználó kitölt egy regisztrációs ûrlapot a kívánt felhasználónevével és jelszavával, és elküldi az ûrlapot a szerverre. A szerver ezután kivonatolja a jelszót, és eltárolja az adatbázisban. A kivonatolás megakadályozza a jelszólopást. Még ha egy támadó feltöri is a rendszert, és hozzáfér a rendszeréhez, akkor sem tudja ellopni a jelszavakat, mert azok kivonatolva vannak. A kivonatolás eltér a titkosítástól.
            // Titkosítás vs kivonatolás
            // A titkosítás visszafordítható, azaz ami titkosítva van, az visszafejthetõ. A kivonat egyirányú. Visszafordíthatatlan. A kivonat összekeveri az egyszerû szöveget, hogy egyedi üzenetkivonatot készítsen. Ha erõs algoritmussal hajtják végre, akkor nincs mód a kivonatolási folyamat visszafordítására, hogy felfedje az eredeti jelszót.
            // A támadó véletlenszerû jelszavakat kivonatozhat, majd összehasonlíthatja a kivonatokat a jelszó feltöréséhez.
            // Ha véletlenszerû sót adunk a kivonatolási folyamathoz, a generált hash nem lesz ugyanaz, még akkor sem, ha az egyszerû szöveges jelszavak azok.
            // Bejelentkezés
            // A regisztrált felhasználónév és jelszó ezután használható a bejelentkezési ûrlapon. A bejelentkezési ûrlap felkerül a szerverre. A szerver megkeresi a felhasználónevet az adatbázisban. Kivonatolja a megadott jelszót, és összehasonlítja az adatbázisban már kivonatolt jelszóval. Ha egyeznek, akkor a rendszer tudja, hogy a felhasználó az, akinek állítja magát, ellenkezõ esetben a hozzáférést megtagadja a 401-es HTTP állapotkód elküldésével.
            // Ha a megadott felhasználónév és jelszó egyezik, a szerver létrehoz egy hozzáférési tokent, amely egyedileg azonosítja a felhasználó munkamenetét. Ez a hozzáférési token az adatbázisban tárolódik, és a válasz cookie-hoz is csatolva van. Ez a cookie ezután visszakerül az ügyfélnek. A felhasználó most be van jelentkezve.
            // Utólagos kérések
            // A böngészõ minden további kérésre automatikusan elküldi a cookie-t a szervernek. A szerver beolvassa a hozzáférési jogkivonatot a cookie-ból, és összeveti az adott felhasználóhoz társított adatbázisban lévõvel. Ha megegyeznek, a hozzáférés biztosított.
            // Miután a felhasználó kijelentkezik az alkalmazásból, mind a hitelesítési cookie, mind a hozzáférési jogkivonat törlõdik az adatbázisban.
            // Cookie-hitelesítés vagy token hitelesítés
            // A következõ a Blazor alkalmazás architektúrája. Van egy Blazor webalkalmazásunk és egy webes API-nk. A Blazor webalkalmazás WEB API-t hív. Client <-> Blazor Server <-> Restful Service <-> Database
            //Attól függõen, hogy hogyan szeretné skálázni az alkalmazást, a blazor webalkalmazás és a webes API is telepítve lehet ugyanazon a kiszolgálón vagy különbözõ kiszolgálókon. Az igényektõl függõen, ha önállóan szeretné fel- és lefelé skálázni ezt a két alkalmazást, akkor elõfordulhat, hogy különbözõ kiszolgálókra kell telepítenie õket.
            //Ha különbözõ szervereken vannak telepítve, akkor nem tudjuk ugyanazt a cookie-hitelesítést használni a Blazor webalkalmazás és a webes API hitelesítésére. Ennek az az oka, hogy az egyik tartomány által létrehozott cookie-hoz nem férhet hozzá egy másik tartomány.
            //Bár lehetséges a cookie-k megosztása az aldomainek között, bevett gyakorlat a cookie-alapú hitelesítés használata webes alkalmazásokhoz és token alapú hitelesítés a webes apis-okhoz.
            //Következõ videóinkban süti-hitelesítést fogunk megvalósítani, hogy megvédjük blázor webalkalmazásunkat, és token hitelesítést webes API-nk védelme érdekében.
            #endregion
            builder.Services.AddAuthentication("Identity.Application").AddCookie();

            #region Comment
            // EN
            // Right click on the Blazor web project and select Add - New Scaffolded Item...
            // Select from both the left and middle panes in Item dialog.IdentityAdd New Scaffolded
            // On Add Identity dialog
            // Check - This adds all the Identity views and classes to our project, so we can modify them to meet our application specific requirements.Override all files
            // Data context class - If your project already has a data context class, select it from the dropdown list.Otherwise, click the plus sign to have it generated.
            // Click Add
            // The generated identity files are stored in folder.Areas/Identity
            // Add and execute identity migrations
            // Open and execute the following command add a migrationPackage Manager Console
            // Add - Migration IdentitySupport
            // Execute the migration
            // Update - Database
            // GE
            // Klicken Sie mit der rechten Maustaste auf das Blazor-Webprojekt und wählen Sie Add - New Scaffolded Item...
            // Wählen Sie aus dem linken und dem mittleren Bereich im Dialogfeld „Element“ aus.IdentitätNeues Gerüst hinzufügen
            // Im Dialogfeld "Identität hinzufügen".
            // Check - Dies fügt alle Identitätsansichten und -klassen zu unserem Projekt hinzu, sodass wir sie ändern können, um unsere anwendungsspezifischen Anforderungen zu erfüllen. Alle Dateien überschreiben
            // Datenkontextklasse - Wenn Ihr Projekt bereits über eine Datenkontextklasse verfügt, wählen Sie sie aus der Dropdown-Liste aus. Andernfalls klicken Sie auf das Pluszeichen, um sie zu generieren.
            // Klicken Sie auf Hinzufügen
            // Die generierten Identitätsdateien werden in folder.Areas/Identity gespeichert
            // Identitätsmigrationen hinzufügen und ausführen
            // Öffnen Sie den folgenden Befehl und führen Sie ihn aus. Fügen Sie eine MigrationPackage Manager-Konsole hinzu
            // Hinzufügen - Migration IdentitySupport : Add - Migration IdentitySupport
            // Migration ausführen
            // Datenbank auf den neusten Stand bringen : Update - Database
            // HU
            // Kattintson a jobb gombbal a Blazor webes projektre, és válassza a lehetõséget Add - New Scaffolded Item...
            // Válasszon mind a bal, mind a középsõ ablaktáblák közül az Elem párbeszédpanelen.IdentityAdd New Scaffolded
            // Az Identitás hozzáadása párbeszédpanelen
            // Check - Ezzel hozzáadja az összes Identity nézetet és osztályt a projektünkhöz, így módosíthatjuk õket, hogy megfeleljenek az alkalmazásspecifikus követelményeinknek. Minden fájl felülbírálása
            // Adatkörnyezetosztály - Ha a projektnek már van adatkörnyezet-osztálya, válassza ki azt a legördülõ listából. Ellenkezõ esetben kattintson a pluszjelre a létrehozásához.
            // Kattintson a Hozzáadás gombra
            // A generált identitásfájlok a mappában tárolódnak.Areas/Identity
            // Identitásáttelepítések hozzáadása és végrehajtása
            // Nyissa meg és hajtsa végre a következõ parancsot add a migrationPackage Manager Console
            // Hozzáadás - Migration IdentitySupport : Add - Migration IdentitySupport
            // Az áttelepítés végrehajtása
            // Adatbázis frissítése : Update - Database
            #endregion
            var connectionString = builder.Configuration.GetConnectionString("EmployeeManagementWebContextConnection") ?? throw new InvalidOperationException("Connection string 'EmployeeManagementWebContextConnection' not found.");

            builder.Services.AddDbContext<EmployeeManagementWebContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<EmployeeManagementWebContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            // Port Number: API port
            builder.Services.AddHttpClient<IEmployeeService, EmployeeService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7036/");
            });
            builder.Services.AddHttpClient<IDepartmentService, DepartmentService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7036/");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            // EN
            // In method of the class, and methods add authentication and authorization middleware components to the request processing pipeline.
            // GE
            // In-Methode der Klasse und Methoden fügen der Anforderungsverarbeitungspipeline Authentifizierungs- und Autorisierungs-Middleware-Komponenten hinzu.
            // HU
            // Az osztály metódusában és metódusaiban hitelesítési és engedélyezési middleware-összetevõket ad hozzá a kérelemfeldolgozási folyamathoz.
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.UseAuthentication();;

            app.Run();
        }
    }
}