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

        /// <summary>
        /// Get user by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get paginated list of users
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagedUserResultRequestDto input)
        {
            var result = await _userService.GetAllAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto input)
        {
            var result = await _userService.CreateAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto input)
        {
            var result = await _userService.UpdateAsync(id, input);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
