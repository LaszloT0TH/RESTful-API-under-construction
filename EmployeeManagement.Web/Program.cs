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
            // Um sich anmelden zu k�nnen, ben�tigt ein Benutzer ein Benutzerkonto bei unserem System. Der Benutzer f�llt also ein Registrierungsformular mit seinem bevorzugten Benutzernamen und Passwort aus und sendet das Formular an den Server. Der Server hasht dann das Passwort und speichert es in der Datenbank. Hashing verhindert Passwortdiebstahl. Selbst wenn ein Angreifer Ihr System knackt und sich Zugriff verschafft, kann er keine Passw�rter stehlen, da sie gehasht sind. Hashing unterscheidet sich von Verschl�sselung.
            // Verschl�sselung vs. Hashing
            // Die Verschl�sselung ist reversibel, d. h. was verschl�sselt ist, kann auch entschl�sselt werden. Hashing ist einseitig. Es ist irreversibel. Wenn es mit einem starken Algorithmus implementiert wird, gibt es keine M�glichkeit, den Hash-Prozess umzukehren, um das urspr�ngliche Passwort zu enth�llen.
            // Ein Angreifer kann zuf�llige Passw�rter hashen und dann die Hashes vergleichen, um das Passwort zu knacken.
            // Wenn dem Hash-Prozess ein zuf�lliges Salt hinzugef�gt wird, ist der generierte Hash nicht derselbe, selbst wenn die Klartext-Passw�rter es sind.
            // Benutzer-Anmeldung
            // Der registrierte Benutzername und das Passwort k�nnen dann im Anmeldeformular verwendet werden. Das Anmeldeformular wird an den Server gesendet. Der Server sucht den Benutzernamen in der Datenbank. Hasht das bereitgestellte Passwort und vergleicht es mit dem bereits gehashten Passwort in der Datenbank. Wenn sie �bereinstimmen, wei� das System, dass der Benutzer derjenige ist, f�r den er sich ausgibt, andernfalls wird der Zugriff durch Senden des HTTP-Statuscodes 401 verweigert.
            // Wenn der angegebene Benutzername und das Passwort �bereinstimmen, erstellt der Server ein Zugriffstoken, das die Sitzung des Benutzers eindeutig identifiziert. Dieses Zugriffstoken wird in der Datenbank gespeichert und auch an das Antwort-Cookie angeh�ngt. Dieses Cookie wird dann an den Client zur�ckgegeben. Der Benutzer ist jetzt angemeldet.
            // Nachfolgende Anfragen
            // Bei jeder weiteren Anfrage sendet der Browser das Cookie automatisch an den Server. Der Server liest das Zugriffstoken aus dem Cookie und vergleicht es mit dem Token in der Datenbank, das diesem Benutzer zugeordnet ist. Bei �bereinstimmung wird der Zugriff gew�hrt.
            // Sobald sich der Benutzer von der Anwendung abmeldet, werden sowohl das Authentifizierungs-Cookie als auch das Zugriffstoken in der Datenbank gel�scht.
            // Cookie-Authentifizierung oder Token-Authentifizierung
            // Das Folgende ist unsere Blazor-Anwendungsarchitektur. Wir haben eine Blazor-Webanwendung und eine Web-API. Die Blazor-Webanwendung ruft die WEB-API auf. Client <-> Blazor Server <-> Restful Service <-> Datenbank
            //Je nachdem, wie Sie Ihre Anwendung skalieren m�chten, k�nnen Sie sowohl die Blazor-Web-App als auch die Web-API auf demselben Server oder auf verschiedenen Servern bereitstellen. Wenn Sie diese beiden Anwendungen unabh�ngig voneinander hoch- und herunterskalieren m�chten, m�ssen Sie sie je nach Bedarf m�glicherweise auf verschiedenen Servern bereitstellen.
            //Wenn sie auf verschiedenen Servern bereitgestellt werden, k�nnen wir nicht dieselbe Cookie-Authentifizierung verwenden, um sowohl die Blazor-Webanwendung als auch die Web-API zu authentifizieren. Dies liegt daran, dass auf ein von einer Dom�ne erstelltes Cookie von einer anderen Dom�ne nicht zugegriffen werden kann.
            //Obwohl es m�glich ist, Cookies zwischen Subdom�nen zu teilen, ist es eine Standardpraxis, die Cookie-basierte Authentifizierung f�r Webanwendungen und die Token-basierte Authentifizierung f�r Web-APIs zu verwenden.
            //In unseren kommenden Videos werden wir die Cookie-Authentifizierung zum Schutz unserer Blazor-Webanwendung und die Token-Authentifizierung zum Schutz unserer Web-API implementieren.
            // HU
            // Felhaszn�l� regisztr�ci�
            // Ahhoz, hogy a felhaszn�l� bejelentkezhessen, felhaszn�l�i fi�kra van sz�ks�ge a rendszer�nkben. �gy a felhaszn�l� kit�lt egy regisztr�ci�s �rlapot a k�v�nt felhaszn�l�nev�vel �s jelszav�val, �s elk�ldi az �rlapot a szerverre. A szerver ezut�n kivonatolja a jelsz�t, �s elt�rolja az adatb�zisban. A kivonatol�s megakad�lyozza a jelsz�lop�st. M�g ha egy t�mad� felt�ri is a rendszert, �s hozz�f�r a rendszer�hez, akkor sem tudja ellopni a jelszavakat, mert azok kivonatolva vannak. A kivonatol�s elt�r a titkos�t�st�l.
            // Titkos�t�s vs kivonatol�s
            // A titkos�t�s visszaford�that�, azaz ami titkos�tva van, az visszafejthet�. A kivonat egyir�ny�. Visszaford�thatatlan. A kivonat �sszekeveri az egyszer� sz�veget, hogy egyedi �zenetkivonatot k�sz�tsen. Ha er�s algoritmussal hajtj�k v�gre, akkor nincs m�d a kivonatol�si folyamat visszaford�t�s�ra, hogy felfedje az eredeti jelsz�t.
            // A t�mad� v�letlenszer� jelszavakat kivonatozhat, majd �sszehasonl�thatja a kivonatokat a jelsz� felt�r�s�hez.
            // Ha v�letlenszer� s�t adunk a kivonatol�si folyamathoz, a gener�lt hash nem lesz ugyanaz, m�g akkor sem, ha az egyszer� sz�veges jelszavak azok.
            // Bejelentkez�s
            // A regisztr�lt felhaszn�l�n�v �s jelsz� ezut�n haszn�lhat� a bejelentkez�si �rlapon. A bejelentkez�si �rlap felker�l a szerverre. A szerver megkeresi a felhaszn�l�nevet az adatb�zisban. Kivonatolja a megadott jelsz�t, �s �sszehasonl�tja az adatb�zisban m�r kivonatolt jelsz�val. Ha egyeznek, akkor a rendszer tudja, hogy a felhaszn�l� az, akinek �ll�tja mag�t, ellenkez� esetben a hozz�f�r�st megtagadja a 401-es HTTP �llapotk�d elk�ld�s�vel.
            // Ha a megadott felhaszn�l�n�v �s jelsz� egyezik, a szerver l�trehoz egy hozz�f�r�si tokent, amely egyedileg azonos�tja a felhaszn�l� munkamenet�t. Ez a hozz�f�r�si token az adatb�zisban t�rol�dik, �s a v�lasz cookie-hoz is csatolva van. Ez a cookie ezut�n visszaker�l az �gyf�lnek. A felhaszn�l� most be van jelentkezve.
            // Ut�lagos k�r�sek
            // A b�ng�sz� minden tov�bbi k�r�sre automatikusan elk�ldi a cookie-t a szervernek. A szerver beolvassa a hozz�f�r�si jogkivonatot a cookie-b�l, �s �sszeveti az adott felhaszn�l�hoz t�rs�tott adatb�zisban l�v�vel. Ha megegyeznek, a hozz�f�r�s biztos�tott.
            // Miut�n a felhaszn�l� kijelentkezik az alkalmaz�sb�l, mind a hiteles�t�si cookie, mind a hozz�f�r�si jogkivonat t�rl�dik az adatb�zisban.
            // Cookie-hiteles�t�s vagy token hiteles�t�s
            // A k�vetkez� a Blazor alkalmaz�s architekt�r�ja. Van egy Blazor webalkalmaz�sunk �s egy webes API-nk. A Blazor webalkalmaz�s WEB API-t h�v. Client <-> Blazor Server <-> Restful Service <-> Database
            //Att�l f�gg�en, hogy hogyan szeretn� sk�l�zni az alkalmaz�st, a blazor webalkalmaz�s �s a webes API is telep�tve lehet ugyanazon a kiszolg�l�n vagy k�l�nb�z� kiszolg�l�kon. Az ig�nyekt�l f�gg�en, ha �n�ll�an szeretn� fel- �s lefel� sk�l�zni ezt a k�t alkalmaz�st, akkor el�fordulhat, hogy k�l�nb�z� kiszolg�l�kra kell telep�tenie �ket.
            //Ha k�l�nb�z� szervereken vannak telep�tve, akkor nem tudjuk ugyanazt a cookie-hiteles�t�st haszn�lni a Blazor webalkalmaz�s �s a webes API hiteles�t�s�re. Ennek az az oka, hogy az egyik tartom�ny �ltal l�trehozott cookie-hoz nem f�rhet hozz� egy m�sik tartom�ny.
            //B�r lehets�ges a cookie-k megoszt�sa az aldomainek k�z�tt, bevett gyakorlat a cookie-alap� hiteles�t�s haszn�lata webes alkalmaz�sokhoz �s token alap� hiteles�t�s a webes apis-okhoz.
            //K�vetkez� vide�inkban s�ti-hiteles�t�st fogunk megval�s�tani, hogy megv�dj�k bl�zor webalkalmaz�sunkat, �s token hiteles�t�st webes API-nk v�delme �rdek�ben.
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
            // Klicken Sie mit der rechten Maustaste auf das Blazor-Webprojekt und w�hlen Sie Add - New Scaffolded Item...
            // W�hlen Sie aus dem linken und dem mittleren Bereich im Dialogfeld �Element� aus.Identit�tNeues Ger�st hinzuf�gen
            // Im Dialogfeld "Identit�t hinzuf�gen".
            // Check - Dies f�gt alle Identit�tsansichten und -klassen zu unserem Projekt hinzu, sodass wir sie �ndern k�nnen, um unsere anwendungsspezifischen Anforderungen zu erf�llen. Alle Dateien �berschreiben
            // Datenkontextklasse - Wenn Ihr Projekt bereits �ber eine Datenkontextklasse verf�gt, w�hlen Sie sie aus der Dropdown-Liste aus. Andernfalls klicken Sie auf das Pluszeichen, um sie zu generieren.
            // Klicken Sie auf Hinzuf�gen
            // Die generierten Identit�tsdateien werden in folder.Areas/Identity gespeichert
            // Identit�tsmigrationen hinzuf�gen und ausf�hren
            // �ffnen Sie den folgenden Befehl und f�hren Sie ihn aus. F�gen Sie eine MigrationPackage Manager-Konsole hinzu
            // Hinzuf�gen - Migration IdentitySupport : Add - Migration IdentitySupport
            // Migration ausf�hren
            // Datenbank auf den neusten Stand bringen : Update - Database
            // HU
            // Kattintson a jobb gombbal a Blazor webes projektre, �s v�lassza a lehet�s�get Add - New Scaffolded Item...
            // V�lasszon mind a bal, mind a k�z�ps� ablakt�bl�k k�z�l az Elem p�rbesz�dpanelen.IdentityAdd New Scaffolded
            // Az Identit�s hozz�ad�sa p�rbesz�dpanelen
            // Check - Ezzel hozz�adja az �sszes Identity n�zetet �s oszt�lyt a projekt�nkh�z, �gy m�dos�thatjuk �ket, hogy megfeleljenek az alkalmaz�sspecifikus k�vetelm�nyeinknek. Minden f�jl fel�lb�r�l�sa
            // Adatk�rnyezetoszt�ly - Ha a projektnek m�r van adatk�rnyezet-oszt�lya, v�lassza ki azt a leg�rd�l� list�b�l. Ellenkez� esetben kattintson a pluszjelre a l�trehoz�s�hoz.
            // Kattintson a Hozz�ad�s gombra
            // A gener�lt identit�sf�jlok a mapp�ban t�rol�dnak.Areas/Identity
            // Identit�s�ttelep�t�sek hozz�ad�sa �s v�grehajt�sa
            // Nyissa meg �s hajtsa v�gre a k�vetkez� parancsot add a migrationPackage Manager Console
            // Hozz�ad�s - Migration IdentitySupport : Add - Migration IdentitySupport
            // Az �ttelep�t�s v�grehajt�sa
            // Adatb�zis friss�t�se : Update - Database
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
            // In-Methode der Klasse und Methoden f�gen der Anforderungsverarbeitungspipeline Authentifizierungs- und Autorisierungs-Middleware-Komponenten hinzu.
            // HU
            // Az oszt�ly met�dus�ban �s met�dusaiban hiteles�t�si �s enged�lyez�si middleware-�sszetev�ket ad hozz� a k�relemfeldolgoz�si folyamathoz.
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.UseAuthentication();;

            app.Run();
        }
    }
}