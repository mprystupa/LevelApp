using AutoMapper;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.DAL.Models.Core;

namespace LevelApp.BLL.Mappings
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateUserMaps();
        }

        // User
        private void CreateUserMaps()
        {
            // AppUser <-> UserSearchDto
            CreateMap<AppUser, UserSearchDto>()
                .ReverseMap();

            // AppUser <-> UserLoginDto
            CreateMap<AppUser, UserLoginDto>()
                .ForMember(d => d.Password, o => o.Ignore())
                .ReverseMap();
        }
    }
}