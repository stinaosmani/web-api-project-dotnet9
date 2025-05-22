using backend.src.Application.Models.Common.Pagination;

namespace backend.src.Application.Service.Users.Dto
{
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        public string? Keyword { get; set; }
    }
}
