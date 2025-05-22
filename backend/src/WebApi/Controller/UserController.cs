using Application.Users;
using backend.src.Application.Service.Users.Dto;
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

        /// Get paginated list of users
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagedUserResultRequestDto input)
        {
            var result = await _userService.GetAllAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// Get user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// Create a new user
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto input)
        {
            var result = await _userService.CreateAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// Update an existing user
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto input)
        {
            var result = await _userService.UpdateAsync(id, input);
            return StatusCode(result.StatusCode, result);
        }

        /// Delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
