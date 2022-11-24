using KUSYS_Demo.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo.Repositories.Interfaces
{
    public interface IAuthenticationUserService
    {
        //Task<Status> LoginAsync(LoginModel model);
      //  Task LogoutAsync();
        Task<Response> Register(RegisterModel model);

        Task<Response> Login(LoginModel model);


        Task LogoutService();





        // Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);

    }
}
