using KUSYS_Demo.Models.Domain;
using System.Diagnostics.Contracts;

namespace KUSYS_Demo.Models.DTO
{
    public class CoursesStudents
    {


        public string CourseId { get; set; }


        public string Id { get; set; }

        // public virtual Students Students { get; set; }

        public virtual ApplicationUser ApplicationUsers { get; set; }

        public virtual Courses Courses { get; set; }

    }
}
