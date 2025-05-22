using AutoMapper;
using backend.src.Application.Service.Users.Dto;

namespace backend.src.Application.Service.Users.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
