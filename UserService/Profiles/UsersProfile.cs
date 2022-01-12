using AutoMapper;
using UserService.Dtos;
using UserService.Model;

namespace UserService.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
