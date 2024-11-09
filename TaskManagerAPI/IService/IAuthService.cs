using TaskManagerAPI.DTOs.RequestDto;

namespace TaskManagerAPI.IService
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterRequestDto userRequest);
        Task<string> Login(string email, string password);
    }
}
