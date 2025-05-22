using backend.src.Application.Models.Common.Pagination;

namespace backend.src.Application.Service.Posts.Dto
{
    public class PagedPostResultRequestDto : PagedResultRequestDto
    {
        public string? Keyword { get; set; }
    }
}
