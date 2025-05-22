namespace backend.src.Application.Service.Posts.Dto
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public Guid AuthorId { get; set; }
        public string? AuthorName { get; set; }
        
        // Auditable
        public DateTime CreationTime { get; set; }
        public string? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? LastModifierUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? DeleterUserId { get; set; }

    }
}
