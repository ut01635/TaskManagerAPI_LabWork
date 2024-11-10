using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerAPI.DTOs.RequestDto;
using TaskManagerAPI.IRepository;
using TaskManagerAPI.IService;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }
        public async Task<string> Register(UserRegisterRequestDto userRequest)
        {
            var req = new UserLogin
            {
                FullName = userRequest.FullName,
                Email = userRequest.Email,
                Role = userRequest.Role,
                Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password)
            };
            var user = await _authRepository.AddUser(req);
            var token = CreateToken(user);
            return token;
        }

        public async Task<string> Login(LoginRequestDto loginRequest)
        {
            var user = await _authRepository.GetUserByEmail(loginRequest.Email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                throw new Exception("Wrong password.");
            }
            return CreateToken(user);
        }


        private string CreateToken(UserLogin user)
        {
                   var claimsList = new List<Claim>
                    {
                       new Claim("Id", user.UserId.ToString()),
                       new Claim("Name", user.FullName),
                       new Claim("Email", user.Email),
                       new Claim("Role", user.Role.ToString())
                    };



            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
            var credintials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims: claimsList,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credintials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
