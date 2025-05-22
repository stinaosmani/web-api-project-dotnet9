using AutoMapper;
using backend.src.Application.Data;
using backend.src.Application.Models.Common.Pagination;
using backend.src.Application.Models.Common.Response;
using backend.src.Application.Service.Posts.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backend.src.Application.Service.Posts
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post, Guid> _postRepository;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IMapper _mapper;

        public PostService(IRepository<Post, Guid> postRepository, IRepository<User, Guid> userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<PagedResultDto<PostDto>>> GetAllAsync(PagedPostResultRequestDto input)
        {
            var keyword = input.Keyword?.ToLower();

            Expression<Func<Post, bool>> predicate = x =>
                string.IsNullOrEmpty(keyword) ||
                x.Title.ToLower().Contains(keyword) ||
                x.Description.ToLower().Contains(keyword) ||
                x.Slug.ToLower().Contains(keyword);

            var query = _postRepository.AsQueryable()
                .Where(predicate)
                .Where(x => x.IsDeleted == 0)
                .Include(p => p.Author);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            var dto = new PagedResultDto<PostDto>
            {
                TotalCount = totalCount,
                Items = _mapper.Map<List<PostDto>>(items)
            };

            return new Response<PagedResultDto<PostDto>>().Ok(dto);
        }

        public async Task<Response<PostDto>> GetByIdAsync(Guid id)
        {
            var post = await _postRepository.AsQueryable()
                .Where(x => x.IsDeleted == 0)
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return new Response<PostDto>().NotFound("Post not found.");

            return new Response<PostDto>().Ok(_mapper.Map<PostDto>(post));
        }

        public async Task<Response<PostDto>> CreateAsync(CreatePostDto input)
        {
            var exists = await _postRepository.AsQueryable()
                .Where(x => x.Slug == input.Slug)
                .Select(_ => 1)
                .FirstOrDefaultAsync() != 0;

            if (exists)
                return new Response<PostDto>().BadRequest("Slug already exists.");

            var authorExists = await _userRepository.AsQueryable()
                .Where(u => u.Id == input.AuthorId)
                .Select(_ => 1)
                .FirstOrDefaultAsync() != 0;

            if (!authorExists)
                return new Response<PostDto>().BadRequest("Author not found.");

            var post = _mapper.Map<Post>(input);
            await _postRepository.AddAsync(post);

            var savedPost = await _postRepository.AsQueryable()
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == post.Id);

            return new Response<PostDto>().Ok(_mapper.Map<PostDto>(savedPost));
        }

        public async Task<Response<PostDto>> UpdateAsync(Guid id, UpdatePostDto input)
        {
            var post = await _postRepository.AsQueryable()
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return new Response<PostDto>().NotFound("Post not found.");

            _mapper.Map(input, post);
            await _postRepository.UpdateAsync(post);

            var updatedPost = await _postRepository.AsQueryable()
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Id == id);

            return new Response<PostDto>().Ok(_mapper.Map<PostDto>(updatedPost));
        }


        public async Task<Response<bool>> DeleteAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                return new Response<bool>().NotFound("Post not found.");

            post.IsDeleted = 1; // Trigger soft-delete

            await _postRepository.UpdateAsync(post);
            return new Response<bool>().NoContent(true);
        }
    }
}
