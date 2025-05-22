using backend.src.Application.Models.Common.Pagination;
using backend.src.Application.Models.Common.Response;
using backend.src.Application.Service.Posts.Dto;

namespace backend.src.Application.Service.Posts
{
    public interface IPostService
    {
        Task<Response<PagedResultDto<PostDto>>> GetAllAsync(PagedPostResultRequestDto input);
        Task<Response<PostDto>> GetByIdAsync(Guid id);
        Task<Response<PostDto>> CreateAsync(CreatePostDto input);
        Task<Response<PostDto>> UpdateAsync(Guid id, UpdatePostDto input);
        Task<Response<bool>> DeleteAsync(Guid id);
    }

}
