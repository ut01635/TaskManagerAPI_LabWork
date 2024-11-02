using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models
{
    public class UserLogin
    {
        [Key]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public Role Role { get; set; }
    }

    public enum Role
    {
        Admin = 1,
        Editor = 2,
        Viewer = 3
    }
}
