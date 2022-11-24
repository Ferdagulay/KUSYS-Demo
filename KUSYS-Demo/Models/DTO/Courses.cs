using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models.DTO
{
    public class Courses
    {
        [Key]
        public string CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }


        public virtual ICollection<CoursesStudents> CoursesStudents { get; set; }

    }
}
