namespace backend.src.Application.Service.Posts.Dto
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public Guid AuthorId { get; set; }
    }
}
