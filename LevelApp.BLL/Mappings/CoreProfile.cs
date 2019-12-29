using AutoMapper;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.DAL.Models.Core;

namespace LevelApp.BLL.Mappings
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateUserMaps();
            CreateLessonMaps();
        }

        // User
        private void CreateUserMaps()
        {
            // AppUser <-> UserSearchDto
            CreateMap<AppUser, UserSearchDto>()
                .ForMember(x => x.Permissions, o => o.Ignore())
                .ReverseMap();

            // AppUser <-> UserLoginDto
            CreateMap<AppUser, UserLoginDto>()
                .ForMember(d => d.Password, o => o.Ignore())
                .ForMember(x => x.Permissions, o => o.Ignore())
                .ReverseMap();
        }
        
        // Lesson
        private void CreateLessonMaps()
        {
            // Lesson <-> LessonDto
            CreateMap<Lesson, LessonDto>()
                .ForMember(x => x.Permissions, o => o.Ignore())
                .ReverseMap();
            
            // Lesson <-> LessonSearchEntryDto
            CreateMap<Lesson, LessonSearchEntryDto>()
                .ForMember(x => x.LessonStatus, o => o.Ignore())
                .ForMember(x => x.IsFavourite, o => o.Ignore())
                .ForMember(x => x.Permissions, o => o.Ignore())
                .ReverseMap()
                .ForAllOtherMembers(x => x.Ignore());
        }
    }
}