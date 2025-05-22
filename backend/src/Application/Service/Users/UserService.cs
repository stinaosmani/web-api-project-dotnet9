using Application.Users;
using AutoMapper;
using backend.src.Application.Data;
using backend.src.Application.Models.Common.Pagination;
using backend.src.Application.Models.Common.Response;
using backend.src.Application.Service.Users.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backend.src.Application.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User, Guid> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<PagedResultDto<UserDto>>> GetAllAsync(PagedUserResultRequestDto input)
        {
            var keyword = input.Keyword?.ToLower();

            Expression<Func<User, bool>> predicate = x =>
                string.IsNullOrEmpty(keyword) ||
                x.Username.ToLower().Contains(keyword) ||
                x.FirstName.ToLower().Contains(keyword) ||
                x.LastName.ToLower().Contains(keyword);

            var query = _userRepository.AsQueryable().Where(predicate);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var dto = new PagedResultDto<UserDto>
            {
                TotalCount = totalCount,
                Items = _mapper.Map<List<UserDto>>(items)
            };

            return new Response<PagedResultDto<UserDto>>().Ok(dto);
        }

        public async Task<Response<UserDto>> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return new Response<UserDto>().NotFound("User not found.");

            var dto = _mapper.Map<UserDto>(user);
            return new Response<UserDto>().Ok(dto);
        }

        public async Task<Response<UserDto>> CreateAsync(CreateUserDto input)
        {
            var exists = await _userRepository.AsQueryable()
                .Where(x => x.Username == input.Username)
                .Select(_ => 1)
                .FirstOrDefaultAsync() != 0;

            if (exists)
                return new Response<UserDto>().BadRequest("Username already exists.");

            var user = _mapper.Map<User>(input);
            await _userRepository.AddAsync(user);

            var dto = _mapper.Map<UserDto>(user);
            return new Response<UserDto>().Ok(dto);
        }

        public async Task<Response<UserDto>> UpdateAsync(Guid id, UpdateUserDto input)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return new Response<UserDto>().NotFound("User not found.");

            _mapper.Map(input, user);
            _userRepository.Update(user);

            var dto = _mapper.Map<UserDto>(user);
            return new Response<UserDto>().Ok(dto);
        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return new Response<bool>().NotFound("User not found.");

            _userRepository.Delete(user);
            return new Response<bool>().NoContent(true);
        }
    }
}
