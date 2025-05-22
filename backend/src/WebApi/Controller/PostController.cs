using backend.src.Application.Service.Posts;
using backend.src.Application.Service.Posts.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        /// Get paginated list of posts
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagedPostResultRequestDto input)
        {
            var result = await _postService.GetAllAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// Get a post by ID
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _postService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        /// Create a new post
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDto input)
        {
            var result = await _postService.CreateAsync(input);
            return StatusCode(result.StatusCode, result);
        }

        /// Update an existing post
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePostDto input)
        {
            var result = await _postService.UpdateAsync(id, input);
            return StatusCode(result.StatusCode, result);
        }

        /// Delete a post
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _postService.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
