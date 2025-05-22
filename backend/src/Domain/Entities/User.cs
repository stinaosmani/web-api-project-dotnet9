using backend.src.Application.Core.Shared;
using System.ComponentModel.DataAnnotations.Schema;

[Table("USERS")]
public class User : FullAuditedEntity
{
    [Column("ID")]
    public Guid Id { get; set; }

    [Column("USERNAME")]
    public string Username { get; set; }

    [Column("PASSWORD")]
    public string Password { get; set; }

    [Column("FIRSTNAME")]
    public string FirstName { get; set; }

    [Column("LASTNAME")]
    public string LastName { get; set; }

    [Column("DATEOFBIRTH")]
    public DateTime DateOfBirth { get; set; }

    [Column("SETTINGSJSON")]
    public string? SettingsJson { get; set; }

    public ICollection<Post> Posts { get; set; }
}
