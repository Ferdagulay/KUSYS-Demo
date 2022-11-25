using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models.DTO
{
    public class Students

    {
        //This table is out of use. Ignore this table.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StudentId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        //public virtual ICollection<CoursesStudents> CoursesStudents { get; set; }



    }
}
