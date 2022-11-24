using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Models.DTO;
using KUSYS_Demo.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KUSYS_Demo.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IService<ApplicationUser> _studentsRepository;



        public StudentController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager, IService<ApplicationUser> studentsRepository)
        {
            _context = Context;
            _userManager = userManager;
            _studentsRepository = studentsRepository;


        }
        //public async Task<IActionResult> Index()
        //{

        //    var Student = await _context.Students.CountAsync();

        //    if (Student == null)
        //    {
        //        return NotFound();
        //    }

        //    return View();
        //}


        //public ActionResult Index()
        //{


        //    /* var std = db.Pages.Where(s => s.PageId == pages.PageId ).FirstOrDefault();*/
        //    return View(_context.Students.ToList());
        //}



        [HttpGet]
        [ProducesResponseType(typeof(List<ApplicationUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Index()
        {
            var studentsList = await _studentsRepository.GetAllStudents();

            if (studentsList is null)
            {
                return Ok();
            }

            return View(studentsList);
        }


        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentsRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }





        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromBody] ApplicationUser student)
        {
            if ( student is null)
            {
                return BadRequest();
            }



            var oldStudent = await _studentsRepository.GetById(id);

            var updatedStudent = new ApplicationUser
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate

            };

            await _userManager.UpdateAsync(updatedStudent);

           // await _studentsRepository.Update(updatedStudent, oldStudent);
            return CreatedAtAction(nameof(Edit), new { Id = student.Id }, null);



        }







        //[HttpPost("{id}")]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType((int)HttpStatusCode.Created)]
        //public async Task<ActionResult> Edit(int id, [FromBody] ApplicationUser student)
        //{
        //    if (id < 1 || student is null)
        //    {
        //        return BadRequest();
        //    }

        //    var oldStudent = await _studentsRepository.GetById(id);

        //    var updatedStudent = new ApplicationUser
        //    {
        //        Id = student.Id,
        //        FirstName = student.FirstName,
        //        LastName = student.LastName,
        //        BirthDate = student.BirthDate

        //    };

        //    await _studentsRepository.Update(updatedStudent, oldStudent);

        //    return CreatedAtAction(nameof(Edit), new { Id = student.Id}, null);
        //}










    }

}
