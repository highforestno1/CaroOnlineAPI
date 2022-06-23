using System.Threading.Tasks;
using Lab01.Models.ViewModels;
using Lab01.RequestModels;

namespace Lab01.Services
{
    public interface IUserService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<bool> Registration(RegistrationUser request);
    }
}