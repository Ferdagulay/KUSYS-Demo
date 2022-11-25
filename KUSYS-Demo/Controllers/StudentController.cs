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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IService<ApplicationUser> _studentsRepository;



        public StudentController(UserManager<ApplicationUser> userManager, IService<ApplicationUser> studentsRepository)
        {
            _userManager = userManager;
            _studentsRepository = studentsRepository;
        }
    

        [HttpGet]
        [ProducesResponseType(typeof(List<ApplicationUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Index()
        {
            var studentsList = await _studentsRepository.GetAll();

            if (studentsList is null)
            {
                return Ok();
            }

            return View(studentsList);
        }




        public async Task<JsonResult> GetById(string id)
        {
            var studentsList = await _studentsRepository.GetById(id);

            return Json(studentsList);
            //return new JsonResult(Ok());
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
            return CreatedAtAction(nameof(Edit), new { Id = student.Id }, null);
        }














    }

}
