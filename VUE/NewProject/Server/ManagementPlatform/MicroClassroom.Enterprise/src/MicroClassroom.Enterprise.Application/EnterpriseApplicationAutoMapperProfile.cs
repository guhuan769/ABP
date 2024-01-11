using AutoMapper;

namespace MicroClassroom.Enterprise;

public class EnterpriseApplicationAutoMapperProfile : Profile
{
    public EnterpriseApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Mechanism, MechanismDto>();
        CreateMap<MechanismDto, MechanismDto>();

        CreateMap<CourseDto, Course>()
            .ForMember(dest => dest.TenantId, src => src.Ignore())
            .ForMember(dest => dest.CourseTeachers, src => src.Ignore())
            .ForMember(dest => dest.ExtraProperties, src => src.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore());
        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.Teachers, src => src.Ignore())
            .ForMember(dest => dest.CourseItems, src => src.Ignore());

        CreateMap<CourseItem, CourseItemDto>();
        CreateMap<CourseItemDto, CourseItem>()
            .ForMember(dest => dest.TenantId, src => src.Ignore());

        CreateMap<Teacher, TeacherDto>();
        CreateMap<TeacherDto, Teacher>()
            .ForMember(dest => dest.TenantId, src => src.Ignore());

        CreateMap<Banner, BannerDto>();
        CreateMap<BannerDto, Banner>()
            .ForMember(dest => dest.TenantId, src => src.Ignore());

        CreateMap<CourseCategory, CourseCategoryDto>();
        CreateMap<CourseCategoryDto, CourseCategory>()
            .ForMember(dest => dest.TenantId, src => src.Ignore())
            .ForMember(dest => dest.Courses, src => src.Ignore());

        CreateMap<CourseCategoryInput, CourseCategory>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.TenantId, src => src.Ignore())
            .ForMember(dest => dest.Courses, src => src.Ignore());

        CreateMap<ImportMechanismDto, Mechanism>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.Pinyin, src => src.Ignore())
            .ForMember(dest => dest.Grade, src => src.Ignore())
            .ForMember(dest => dest.TenantId, src => src.Ignore())
            .ForMember(dest => dest.ExtraProperties, src => src.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore())
            .ForMember(dest => dest.Banners, src => src.Ignore())
            .ForMember(dest => dest.Teachers, src => src.Ignore())
            .ForMember(dest => dest.Courses, src => src.Ignore());

        CreateMap<ImportTeacherDto, Teacher>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.MechanismId, src => src.Ignore())
            .ForMember(dest => dest.TenantId, src => src.Ignore());

        CreateMap<ImportCourseCategoryDto, CourseCategory>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.TenantId, src => src.Ignore())
            .ForMember(dest => dest.Courses, src => src.Ignore());

        CreateMap<ImportCourseDto, Course>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.MechanismId, src => src.Ignore())
            .ForMember(dest => dest.CategoryId, src => src.Ignore())
            .ForMember(dest => dest.TenantId, src => src.Ignore())
            .ForMember(dest => dest.ExtraProperties, src => src.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore())
            .ForMember(dest => dest.CourseItems, src => src.Ignore())
            .ForMember(dest => dest.CourseTeachers, src => src.Ignore());

        CreateMap<ImportCourseItemDto, CourseItem>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.TenantId, src => src.Ignore());

    }
}
