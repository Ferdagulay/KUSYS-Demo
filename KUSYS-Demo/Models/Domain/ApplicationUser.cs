using KUSYS_Demo.Models.DTO;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        // [Key]
        // public string Id { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        public virtual ICollection<CoursesStudents> CoursesStudents { get; set; }
    }
}
