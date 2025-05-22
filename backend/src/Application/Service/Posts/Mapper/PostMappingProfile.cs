namespace backend.src.Application.Service.Posts.Mapper
{
    using AutoMapper;
    using backend.src.Application.Service.Posts.Dto;

    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            CreateMap<Post, PostDto>()
                 .ForMember(dest => dest.AuthorName,
                            opt => opt.MapFrom(src => src.Author.Username));

            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();
        }
    }
}
