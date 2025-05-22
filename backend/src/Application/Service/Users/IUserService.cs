using backend.src.Application.Models.Common.Pagination;
using backend.src.Application.Models.Common.Response;
using backend.src.Application.Service.Users.Dto;

namespace Application.Users
{
    public interface IUserService
    {
        Task<Response<PagedResultDto<UserDto>>> GetAllAsync(PagedUserResultRequestDto input);
        Task<Response<UserDto>> GetByIdAsync(Guid id);
        Task<Response<UserDto>> CreateAsync(CreateUserDto input);
        Task<Response<UserDto>> UpdateAsync(Guid id, UpdateUserDto input);
        Task<Response<bool>> DeleteAsync(Guid id);
    }

}