using backend.src.Application.Core.Shared;

public class User : FullAuditedEntity
{
    public Guid Id { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? SettingsJson { get; set; }

    public ICollection<Post> Posts { get; set; }
}
