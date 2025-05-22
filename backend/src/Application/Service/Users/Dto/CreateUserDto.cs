using System.ComponentModel.DataAnnotations;

namespace backend.src.Application.Service.Users.Dto
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? SettingsJson { get; set; }
    }
}
