using EmployeeManagement.Models.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    // Install-Package System.ComponentModel.Annotations
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "FirstName is mandatory")]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "sample.com", ErrorMessage = "Base Error Message = Only sample.com is allowed.")]
        public string Email { get; set; }
        public DateTime DateOfBrith { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentId { get; set; }

        // EN
        // Department property in the class is a complex type. To validate this use instead of component. As the name implies, the validates the entire object graph, including collection and complex-type properties.EmployeeObjectGraphDataAnnotationsValidatorDataAnnotationsValidatorObjectGraphDataAnnotationsValidator
        // GE
        // Abteilungseigenschaft in der Klasse ist ein komplexer Typ. Um dies zu validieren, verwenden Sie statt der Komponente. Wie der Name schon sagt, validiert der das gesamte Objektdiagramm, einschließlich Sammlungs- und Komplextypeigenschaften.EmployeeObjectGraphDataAnnotationsValidatorDataAnnotationsValidatorObjectGraphDataAnnotationsValidator
        // HU
        // Department Az osztály tulajdonsága összetett típus. Ennek érvényesítéséhez használja az összetevő helyett. Ahogy a neve is mutatja, a a teljes objektumgráfot érvényesíti, beleértve a gyűjteményt és az összetett típusú tulajdonságokat.EmployeeObjectGraphDataAnnotationsValidatorDataAnnotationsValidatorObjectGraphDataAnnotationsValidator
        // EditEmployee.razor ValidateComplexType & ObjectGraphDataAnnotationsValidator
        // NuGet package install: Microsoft.AspNetCore.Components.DataAnnotations.Validation
        [ValidateComplexType]
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
