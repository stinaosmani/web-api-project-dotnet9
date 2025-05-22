using Application.Users;
using AutoMapper;
using backend.src.Application.Data;
using backend.src.Application.Models.Common.Pagination;
using backend.src.Application.Models.Common.Response;
using backend.src.Application.Service.Auth;
using backend.src.Application.Service.Users.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace backend.src.Application.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserService(IRepository<User, Guid> userRepository, IJwtService jwtService, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<Response<string>> LoginAsync(LoginDto input)
        {
            var user = await _userRepository.AsQueryable()
                .Where(x => x.Username == input.Username && x.IsDeleted == 0)
                .FirstOrDefaultAsync();

            if (user == null)
                return new Response<string>().UnAuthorized("Invalid username or password.");

            var hashedInput = HashPassword(input.Password);
            if (user.Password != hashedInput)
                return new Response<string>().UnAuthorized("Invalid username or password.");

            var token = _jwtService.GenerateToken(user.Id, user.Username, "User");

            return new Response<string>().Ok(token);
        }

        public async Task<Response<PagedResultDto<UserDto>>> GetAllAsync(PagedUserResultRequestDto input)
        {
            var keyword = input.Keyword?.ToLower();

            Expression<Func<User, bool>> predicate = x =>
                string.IsNullOrEmpty(keyword) ||
                x.Username.ToLower().Contains(keyword) ||
                x.FirstName.ToLower().Contains(keyword) ||
                x.LastName.ToLower().Contains(keyword);

            var query = _userRepository.AsQueryable().Where(predicate).Where(x => x.IsDeleted == 0);

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
            var user = await _userRepository.AsQueryable()
                .Where(x => x.Id == id && x.IsDeleted == 0)
                .FirstOrDefaultAsync();

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

            user.Password = HashPassword(input.Password);

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
            await _userRepository.UpdateAsync(user);

            var dto = _mapper.Map<UserDto>(user);
            return new Response<UserDto>().Ok(dto);
        }

        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            if (entity == null)
                return new Response<bool>().NotFound("User not found.");

            entity.IsDeleted = 1; // Trigger soft-delete

            await _userRepository.UpdateAsync(entity); 

            return new Response<bool>().NoContent(true);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
