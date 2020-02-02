using System.Linq;
using AutoMapper;
using LevelApp.BLL.Dto;
using LevelApp.BLL.Dto.Core.Course;
using LevelApp.BLL.Dto.Core.Lesson;
using LevelApp.BLL.Dto.Core.User;
using LevelApp.DAL.Models.Core;

namespace LevelApp.BLL.Mappings
{
    public class CoreProfile : Profile
    {
        private const string TagListDelimiter = "|";
        public CoreProfile()
        {
            CreateUserMaps();
            CreateLessonMaps();
            CreateCourseMaps();
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
            
            // AppUser <-> LessonSearchEntryAuthorDto
            CreateMap<AppUser, LessonSearchEntryAuthorDto>()
                .ForMember(x => x.Permissions, o => o.Ignore());
        }
        
        // Lesson
        private void CreateLessonMaps()
        {
            // Lesson <-> LessonDto
            CreateMap<Lesson, LessonDto>()
                .ForMember(x => x.TagList, o => o.MapFrom(s => s.TagList.Split(TagListDelimiter[0]).ToList()))
                .ForMember(x => x.Permissions, o => o.Ignore())
                .ReverseMap()
                .ForMember(x => x.TagList, o => o.MapFrom(s => string.Join(TagListDelimiter, s.TagList.ToArray())));
            
            // Lesson <-> LessonSearchEntryDto
            CreateMap<Lesson, LessonSearchEntryDto>()
                .ForMember(x => x.LessonStatus, o => o.Ignore())
                .ForMember(x => x.IsFavourite, o => o.Ignore())
                .ForMember(x => x.Course, o => o.Ignore())
                .ForMember(x => x.Permissions, o => o.Ignore());
            
            // Lesson <-> LessonCourseEntryDto
            CreateMap<Lesson, LessonCourseEntryDto>()
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.Permissions, o => o.Ignore())
                .ReverseMap();
        }
        
        // Course
        private void CreateCourseMaps()
        {
            // Course <-> CourseDto
            CreateMap<Course, CourseDto>()
                .ForMember(x => x.Permissions, o => o.Ignore())
                .ForMember(x => x.TagList, o => o.MapFrom(s => s.TagList.Split(TagListDelimiter[0]).ToList()))
                .ForMember(x => x.Lessons, o => o.Ignore())
                .ReverseMap()
                .ForMember(x => x.TagList, o => o.MapFrom(s => string.Join(TagListDelimiter, s.TagList.ToArray())))
                .ForMember(x => x.Lessons, o => o.Ignore());
            
            // Course <-> CourseSearchEntryDto
            CreateMap<Course, CourseSearchEntryDto>()
                .ForMember(x => x.Permissions, o => o.Ignore())
                .ForMember(x => x.CourseStatus, o => o.Ignore())
                .ForMember(x => x.IsFavourite, o => o.Ignore())
                .ReverseMap();
            
            // Course <-> LessonSearchEntryCourseDto
            CreateMap<Course, LessonSearchEntryCourseDto>()
                .ForMember(x => x.Permissions, o => o.Ignore());
        }
    }
}