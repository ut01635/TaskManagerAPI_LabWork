using TaskManagerAPI.Models;

namespace TaskManagerAPI.DTOs.ResponseDto
{
    public class UserLoginResponseDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

    }
}
