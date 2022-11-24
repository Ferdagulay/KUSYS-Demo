
using KUSYS_Demo.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo.Models.Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Courses> Courses { get; set; }
        // public DbSet<Students> Students { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<CoursesStudents> CoursesStudents { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";


            builder.Entity<CoursesStudents>()
                .HasKey(c => new { c.Id, c.CourseId });

            builder.Entity<CoursesStudents>()
                .HasOne<ApplicationUser>(cs => cs.ApplicationUsers)
                .WithMany(c => c.CoursesStudents)
                .HasForeignKey(cs => cs.Id);

            builder.Entity<CoursesStudents>()
                .HasOne<Courses>(cs => cs.Courses)
                .WithMany(c => c.CoursesStudents)
                .HasForeignKey(cs => cs.CourseId);
            
     


            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });



            //create user
            var appUser = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "canan@gmail.com",
                EmailConfirmed = true,
                FirstName = "Canan",
                LastName = "Sağlam",
                UserName = "canan@gmail.com",
             NormalizedUserName = "CANAN@GMAIL.COM"
            };

            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "pass*12");

            //seed user
            builder.Entity<ApplicationUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });




            base.OnModelCreating(builder);



        }




    }
}
