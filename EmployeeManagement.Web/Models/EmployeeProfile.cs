using AutoMapper;
using EmployeeManagement.Models;

namespace EmployeeManagement.Web.Models
{
    /// <summary>
    /// EN
    /// When doing the mapping from one type to another, AutoMapper looks for mapping profiles.
    /// To create a mapping profile, create a class that derives from AutoMapper class.Profile
    /// Use method to create a mapping from one type to another.CreateMap
    /// CreateMap method is called twice to create 2 mappings.to and the reverse.EmployeeEditEmployeeModel
    /// Except for one property, we do not need explicit property mappings, because the property names and their data types match.So the AutoMapper does the mapping for us automatically.(ConfirmEmail)
    /// GE
    /// Bei der Zuordnung von einem Typ zu einem anderen sucht AutoMapper nach Zuordnungsprofilen.
    /// Um ein Zuordnungsprofil zu erstellen, erstellen Sie eine Klasse, die von AutoMapper class.Profile abgeleitet ist
    /// Verwenden Sie die Methode, um eine Zuordnung von einem Typ zu einem anderen zu erstellen.CreateMap
    /// Die CreateMap-Methode wird zweimal aufgerufen, um zwei Zuordnungen zu erstellen.zu und umgekehrt.EmployeeEditEmployeeModel
    /// Bis auf eine Eigenschaft benötigen wir keine expliziten Eigenschaftszuordnungen, da die Eigenschaftsnamen und ihre Datentypen übereinstimmen.Der AutoMapper erledigt das Mapping also automatisch für uns. (ConfirmEmail)
    /// HU
    /// Amikor egyik típusról a másikra végzi a leképezést, az AutoMapper leképezési profilokat keres.
    /// Leképezési profil létrehozásához hozzon létre egy osztályt, amely az AutoMapper osztályból származik.Profile
    /// A metódussal leképezést hozhat létre egyik típusról a másikra.CreateMap
    /// CreateMap metódust kétszer hívják meg 2 leképezés létrehozásához. -hoz és fordítva.EmployeeEditEmployeeModel
    /// Egy tulajdonság kivételével nincs szükség explicit tulajdonságleképezésekre, mert a tulajdonságnevek és azok adattípusai megegyeznek.Tehát az AutoMapper automatikusan elvégzi helyettünk a leképezést.(ConfirmEmail)
    /// </summary>
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            // EN
            // For property in class, we do not have a matching property in class. ConfirmEmailEditEmployeeModelEmployee
            //We have an explicit mapping to map the property in class to property in class.EmailEmployeeConfirmEmailEditEmployeeModel
            // EN
            // Für Eigenschaft in der Klasse haben wir keine passende Eigenschaft in der Klasse. BestätigenE-MailBearbeitenMitarbeiterModellMitarbeiter
            //Wir haben eine explizite Zuordnung, um die Eigenschaft in der Klasse der Eigenschaft in class.EmailEmployeeConfirmEmailEditEmployeeModel zuzuordnen
            // HU
            // Az osztályban lévő ingatlanok esetében nincs megfelelő tulajdonságunk az osztályban. ConfirmEmailEditEmployeeModelEmployee
            // Van egy explicit leképezésünk, amely leképezi az osztályban lévő tulajdonságot az osztályban lévő tulajdonságra.EmailEmployeeConfirmEmail EditEmployeeModel
            CreateMap<Employee, EditEmployeeModel>()
                .ForMember(dest => dest.ConfirmEmail,
                           opt => opt.MapFrom(src => src.Email));
            CreateMap<EditEmployeeModel, Employee>();
        }
    }
}