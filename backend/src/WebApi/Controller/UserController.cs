using Application.Users;
using backend.src.Application.Service.Users.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto input)
        {
            var result = await _userService.LoginAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// Get paginated list of users
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagedUserResultRequestDto input)
        {
            var result = await _userService.GetAllAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// Get user by ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// Create a new user
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto input)
        {
            var result = await _userService.CreateAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// Update an existing user
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto input)
        {
            var result = await _userService.UpdateAsync(id, input);
            return StatusCode(result.StatusCode, result);
        }

        /// Delete a user
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
