using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Repositories.Interfaces;
using KUSYS_Demo.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KUSYS_Demo.Repositories
{
    public class StudentService : IService<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StudentService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }


        public async Task<Response> AddStudent(RegisterModel entity)
        {

            //_context.Add(entity);
            //await _context.SaveChangesAsync();

            // await _userManager.AddToRoleAsync(entity);

            var status = new Response();

            ApplicationUser user = new ApplicationUser()
            {
                Email = entity.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = entity.Username,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, entity.Password);
            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return status;
            }

            if (!await _roleManager.RoleExistsAsync(entity.Role))
                await _roleManager.CreateAsync(new IdentityRole(entity.Role));


            if (await _roleManager.RoleExistsAsync(entity.Role))
            {
                await _userManager.AddToRoleAsync(user, entity.Role);
            }

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;



        }

        public async Task Delete(ApplicationUser entity)
        {
            var user = await _userManager.FindByIdAsync(entity.Id);

            await _userManager.DeleteAsync(user);
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _context.ApplicationUsers.Where(b => b.Id == id).Select(a => new ApplicationUser
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate

            }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllStudents()
        {
            //return await _context.Students.ToListAsync();
            return await _context.ApplicationUsers.Select(a => new ApplicationUser
            {

                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate

            }).ToListAsync();
        }






        public async Task<List<CoursesStudents>> GetSelectedCourseByUserID(string username)
        {

            List<CoursesStudents> coursematch = new List<CoursesStudents>();


            var student = await _userManager.FindByNameAsync(username);

            List<CoursesStudents> courseList = _context.CoursesStudents.Where(a => a.ApplicationUsers.Id == student.Id).ToList();

           

            CoursesStudents cs = new CoursesStudents();
            foreach (var course in courseList)
            {
                var courseobj = _context.Courses.Where(a => a.CourseId == course.CourseId).FirstOrDefault();

                cs.ApplicationUsers = student;
                cs.CourseId = course.CourseId;
                cs.Id = student.Id;
                cs.Courses = courseobj;

                coursematch.Add(cs);

            }

            return coursematch;




        }









        public async Task Update(ApplicationUser dbEntity, ApplicationUser entity)
        {
            dbEntity.BirthDate = entity.BirthDate;
            dbEntity.FirstName = entity.FirstName;
            dbEntity.LastName = entity.LastName;

            // Get the existing student from the db
            var user = await _userManager.FindByIdAsync(dbEntity.Id);

            // Update it with the values from the view model
            user.FirstName = dbEntity.FirstName;
            user.LastName = dbEntity.LastName;
            user.BirthDate = dbEntity.BirthDate;


            // Apply the changes if any to the db
            await _userManager.UpdateAsync(user);



          //  await _userManager.UpdateAsync(dbEntity);
           // _context.ApplicationUsers.Update(dbEntity);
           // await _context.SaveChangesAsync();
        }
    }
}
