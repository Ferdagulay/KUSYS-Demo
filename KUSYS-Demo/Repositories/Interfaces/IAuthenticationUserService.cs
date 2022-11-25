using KUSYS_Demo.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo.Repositories.Interfaces
{
    public interface IAuthenticationUserService
    {
      
        Task<Response> Register(RegisterModel model);

        Task<Response> Login(LoginModel model);


        Task LogoutService();


        // Task<Status> ChangePassword(ChangePasswordModel model, string username); // It can be added.

    }
}
