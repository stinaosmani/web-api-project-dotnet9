using System.ComponentModel.DataAnnotations.Schema;
using backend.src.Application.Core.Shared;

[Table("POSTS")] // Oracle-friendly: all uppercase, unquoted
public class Post : FullAuditedEntity
{
    [Column("ID")]
    public Guid Id { get; set; }

    [Column("TITLE")]
    public string Title { get; set; }

    [Column("DESCRIPTION")]
    public string Description { get; set; }

    [Column("BODY")]
    public string Body { get; set; }

    [Column("SLUG")]
    public string Slug { get; set; }

    [Column("AUTHORID")]
    public Guid AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    public User Author { get; set; }
}
