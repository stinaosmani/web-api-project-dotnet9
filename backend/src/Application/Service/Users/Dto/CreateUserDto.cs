using System.ComponentModel.DataAnnotations;

namespace backend.src.Application.Service.Users.Dto
{
    public class CreateUserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? SettingsJson { get; set; }
    }
}
