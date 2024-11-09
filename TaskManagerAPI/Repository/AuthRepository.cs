using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.IRepository;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TaskContext _dbContext;
        public AuthRepository(TaskContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserLogin> AddUser(UserLogin user)
        {
            var data = await _dbContext.UsersLogin.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return data.Entity;
        }
        public async Task<UserLogin> GetUserByEmail(string email)
        {
            var data = await _dbContext.UsersLogin.SingleOrDefaultAsync(x => x.Email == email);
            return data;
        }
    }
}
