using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace KUSYS_Demo.DbSeeds
{
    public class DbSeed
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.ApplicationUsers.Any())
            {
                return;   // DB has been seeded
            }

            var students = new ApplicationUser[]
            {
                new ApplicationUser{Id = "0", FirstName="Ali",LastName="Kaya",BirthDate=DateTime.Parse("1992-07-30")},
                new ApplicationUser{Id = "1", FirstName="Ayşe",LastName="Sezgin",BirthDate=DateTime.Parse("1992-07-15")},

            };

            context.ApplicationUsers.AddRange(students);
            context.SaveChanges();

            var courses = new Courses[]
            {
                new Courses{CourseId="CSI101",CourseName="Introduction to Computer Science"},
                new Courses{CourseId="CSI102",CourseName="Algorithms"},
                new Courses{CourseId="MAT101",CourseName="Calculus"},
                new Courses{CourseId="PHY101",CourseName="Physics"},

            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var enrollments = new CoursesStudents[]
            {
                new CoursesStudents{Id="1",CourseId="CSI101"},
                new CoursesStudents{Id="1",CourseId="CSI102"},
                new CoursesStudents{Id="1",CourseId="PHY101"},
                new CoursesStudents{Id="0",CourseId="CSI101"},
                new CoursesStudents{Id="0",CourseId="CSI102"},
                new CoursesStudents{Id="0",CourseId="MAT101"},

            };

            context.CoursesStudents.AddRange(enrollments);
            context.SaveChanges();








        }
    }
}
