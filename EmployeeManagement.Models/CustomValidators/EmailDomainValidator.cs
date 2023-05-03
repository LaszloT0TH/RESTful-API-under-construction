using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models.CustomValidators
{
    /// <summary>
    /// EN
    /// Create a class that derives from the built-in abstract class and override method.ValidationAttributeIsValid()
    /// IsValid() method returns if there are no validation errors, otherwise a object.nullValidationResult
    /// ValidationResult accepts 2 parameters - Validation error message and the property name with which this validation error message must be associated with.
    /// The public property() allows to pass the domain name instead of hard-coding it in this class. This approach makes this validator more reusable.AllowedDomainEmailDomainValidator
    /// GE
    /// Erstellen Sie eine Klasse, die von der integrierten abstrakten Klasse abgeleitet ist, und überschreiben Sie die Methode.ValidationAttributeIsValid()
    /// IsValid()-Methode gibt zurück, wenn keine Validierungsfehler vorliegen, andernfalls ein object.nullValidationResult
    /// ValidationResult akzeptiert 2 Parameter - Validierungsfehlermeldung und den Eigenschaftsnamen, dem diese Validierungsfehlermeldung zugeordnet werden muss.
    /// Die öffentliche Eigenschaft () ermöglicht es, den Domänennamen zu übergeben, anstatt ihn in dieser Klasse fest zu codieren. Dieser Ansatz macht diesen Validator besser wiederverwendbar.AllowedDomainEmailDomainValidator
    /// HU
    /// Hozzon létre egy osztályt, amely a beépített absztrakt osztályból és felülírási metódusból származik.ValidationAttributeIsValid()
    /// Az IsValid() metódus akkor tér vissza, ha nincs érvényesítési hiba, ellenkező esetben egy object.nullValidationResult
    /// A ValidationResult 2 paramétert fogad el – Érvényesítési hibaüzenet és a tulajdonságnév, amelyhez ezt az érvényesítési hibaüzenetet társítani kell.
    /// A public property() lehetővé teszi a domain név átadását, ahelyett, hogy ebben az osztályban kódolná. Ez a megközelítés az érvényesítőt újrafelhasználhatóbbá teszi.AllowedDomainEmailDomainValidator
    /// </summary>
    public class EmailDomainValidator : ValidationAttribute
    {
        public string AllowedDomain { get; set; }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                string[] strings = value.ToString().Split('@');
                if (strings.Length > 1 && strings[1].ToUpper() == AllowedDomain.ToUpper())
                {
                    return null;
                }
                // ErrorMessage from base class
                //return new ValidationResult(ErrorMessage,
                //new[] { validationContext.MemberName });

                return new ValidationResult($"Domain must be {AllowedDomain}",
                new[] { validationContext.MemberName });
            }
            return null;
        }
    }
}