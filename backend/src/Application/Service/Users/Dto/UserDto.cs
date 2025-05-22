namespace backend.src.Application.Service.Users.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? SettingsJson { get; set; }

        // Auditing fields
        public DateTime CreationTime { get; set; }
        public string? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? LastModifierUserId { get; set; }
    }

}
