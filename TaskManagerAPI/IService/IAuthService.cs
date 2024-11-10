using TaskManagerAPI.DTOs.RequestDto;

namespace TaskManagerAPI.IService
{
    public interface IAuthService
    {
        Task<string> Register(UserRegisterRequestDto userRequest);
        Task<string> Login(LoginRequestDto loginRequest);
    }
}
