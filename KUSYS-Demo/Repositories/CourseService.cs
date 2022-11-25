using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Repositories.Interfaces;
using KUSYS_Demo.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KUSYS_Demo.Repositories
{
    public class CourseService : IService<Courses>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
            //_userManager = userManager;
            //_roleManager = roleManager;

        }
        public async Task<IEnumerable<Courses>> GetAll()
        {
            return await _context.Courses.ToListAsync();
        }



        public async Task MatchStudent(ApplicationUser user, string courseId )
        {
            var course = await _context.Courses.Where(a =>a.CourseId == courseId).FirstOrDefaultAsync();

            CoursesStudents cs = new CoursesStudents();

            cs.CourseId = course.CourseId;
            cs.Id = user.Id;
            cs.ApplicationUsers = user;
            cs.Courses = course;
            _context.Add(cs);
            await _context.SaveChangesAsync();
        }


     









        public Task Delete(Courses entity)
        {
            throw new NotImplementedException();
        }

  

        public Task<Courses> GetById(string id)
        {

            throw new NotImplementedException();
        }

        public Task Update(Courses dbEntity, Courses entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<CoursesStudents>> GetSelectedCourseByUserID(string username)
        {
            throw new NotImplementedException();
        }
    }
}
