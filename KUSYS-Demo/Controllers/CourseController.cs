using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Models.DTO;
using KUSYS_Demo.Repositories;
using KUSYS_Demo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace KUSYS_Demo.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IService<Courses> _coursesRepository;
        private readonly IService<ApplicationUser> _studentRepository;
        private readonly UserManager<ApplicationUser> _userManager;



        public CourseController(ApplicationDbContext Context,  IService<Courses> coursesRepository, IService<ApplicationUser> studentRepository , UserManager<ApplicationUser> userManager)
        {
            _context = Context;
            _coursesRepository = coursesRepository;
            _userManager = userManager;
            _studentRepository = studentRepository;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<ApplicationUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Courses>>> Index()
        {
            var courseList = await _coursesRepository.GetAll();

            if (courseList is null)
            {
                return Ok();
            }

            return View(courseList);
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        [ProducesResponseType(typeof(List<ApplicationUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Courses>>> SelectedCourseIndex()
        {

            var user = await GetCurrentUserAsync();

            var courseList = await _studentRepository.GetSelectedCourseByUserID(user.UserName);

            if (courseList is null)
            {
                return Ok();
            }

            return View(courseList);
        }


        // POST: Students/Edit/5
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<JsonResult> PostName(string Courseid , string username)
        {

            if (Courseid is null || username is null)
            {
                return  new JsonResult(BadRequest());
            }

            var student = await _userManager.FindByNameAsync(username);

            CourseService cser = new CourseService(_context);

            await cser.MatchStudent(student, Courseid);


            return new JsonResult(Ok());
            //return RedirectToAction(nameof(SelectedCourseIndex), Ok());

        }














    }
}
