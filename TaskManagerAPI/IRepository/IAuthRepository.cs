using TaskManagerAPI.Models;

namespace TaskManagerAPI.IRepository
{
    public interface IAuthRepository
    {
        Task<UserLogin> AddUser(UserLogin user);
        Task<UserLogin> GetUserByEmail(string email);
    }
}
