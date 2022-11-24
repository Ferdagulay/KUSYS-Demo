using KUSYS_Demo.Models.Domain;
using KUSYS_Demo.Models.DTO;
using KUSYS_Demo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KUSYS_Demo.Controllers
{
    public class AuthenticateController : Controller
    {




        private readonly IAuthenticationUserService _authService;
        public AuthenticateController(IAuthenticationUserService authService)
        {
            this._authService = authService;
        }


        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            model.Role = "user";
            var result = await this._authService.Register(model);
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(SignUp));
        }



        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await _authService.Login(model);
            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                TempData["msg"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }





        [Authorize]
        public async Task<IActionResult> LogoutService()
        {
            await this._authService.LogoutService();
            return RedirectToAction(nameof(Login));
        }



     









    }
}
