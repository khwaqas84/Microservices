using AuthService.Models;

namespace AuthService.Services.Interfaces
{
    public interface IAuthserviceRepository
    {
        UserModel ValidateUser(string Email, string password);
        bool CreateUser(SignUpModel user);
    }
}
