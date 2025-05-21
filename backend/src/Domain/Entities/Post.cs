using backend.src.Application.Core.Shared;

public class Post : FullAuditedEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public string Slug { get; set; }

    public Guid AuthorId { get; set; }
    public User Author { get; set; } 
}
