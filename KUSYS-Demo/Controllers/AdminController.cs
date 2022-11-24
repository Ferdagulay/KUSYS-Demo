using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Models.DTO;
using KUSYS_Demo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace KUSYS_Demo.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IService<ApplicationUser> _studentsRepository;


        public AdminController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager, IService<ApplicationUser> studentsRepository)
        {
            _context = Context;
            _userManager = userManager;
            _studentsRepository = studentsRepository;


        }

        //[Authorize(Roles = "admin")]
        //public IActionResult AdminIndex()
        //{
        //    return View();
        //}

   

        [HttpGet]
        [ProducesResponseType(typeof(List<ApplicationUser>), (int)HttpStatusCode.OK)]
        [Authorize(Roles = "admin")]

        public async Task<ActionResult<IEnumerable<ApplicationUser>>> AdminIndex()
            {
                var studentsList = await _studentsRepository.GetAllStudents();

                if (studentsList is null)
                {
                    return Ok();
                }

                return View(studentsList);
            }


        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(string id)
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,BirthDate")] ApplicationUser student)
            {
                if (student is null)
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

                await _studentsRepository.Update(oldStudent , updatedStudent);
            return RedirectToAction(nameof(AdminIndex), null);
           // return RedirectToAction(nameof(AdminIndex));

        }


        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
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



        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Delete(string id, [Bind("Id,FirstName,LastName,BirthDate")] ApplicationUser student)
        {
            if (student is null)
            {
                return BadRequest();
            }
            var dbEntity  = await _studentsRepository.GetById(id);
     
            

             await _studentsRepository.Delete(dbEntity);
            return RedirectToAction(nameof(AdminIndex), null);
        }



        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Students/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDate")] RegisterModel student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }










    }
}
