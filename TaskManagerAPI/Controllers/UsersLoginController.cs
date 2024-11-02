using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTOs.RequestDto;
using TaskManagerAPI.Migrations;
using TaskManagerAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersLoginController : ControllerBase
    {
        private readonly TaskContext _taskContext;

        public UsersLoginController(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(UserRegisterRequestDto request)
        {
            try
            {
                var user = new UserLogin()
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    Password= BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Role = request.Role
                };
                
                var data = await _taskContext.UsersLogin.AddAsync(user);
                await _taskContext.SaveChangesAsync();

                var res = new UserRegisterRequestDto
                {
                    FullName = data.Entity.FullName,
                    Email = data.Entity.Email,
                    Role = data.Entity.Role
                };
                return Ok(res);



            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(string Email,  string password)
        {
            try
            {
                var user = _taskContext.UsersLogin.FirstOrDefault(x => x.Email == Email);
                if(user == null)
                {
                    throw new Exception("User not found");
                }
                
                var isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if(!isValid)
                {
                    throw new Exception("email or password not matched");
                }
                var res = new UserRegisterRequestDto
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role
                };
                return Ok(res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
