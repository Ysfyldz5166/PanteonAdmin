using AutoMapper;
using Panteon.Business.Command.User;
using Panteon.Business.Command.Buildings;
using Panteon.Entities.Dto;
using Panteon.Entities.Entities;

namespace Panteon.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<AddUserCommand, User>();
            CreateMap<GetUserCommand, UserDto>();


            // Build mappings
            CreateMap<Build, BuildDto>().ReverseMap();
            CreateMap<AddBuildCommand, Build>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 
        }
    }
}
