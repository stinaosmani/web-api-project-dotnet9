using System.ComponentModel.DataAnnotations;

namespace backend.src.Application.Service.Users.Dto
{
    public class UpdateUserDto
    {
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? SettingsJson { get; set; }
    }
}
