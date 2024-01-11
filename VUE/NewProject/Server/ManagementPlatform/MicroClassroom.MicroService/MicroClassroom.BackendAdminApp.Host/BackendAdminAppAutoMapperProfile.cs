using AutoMapper;
using MicroClassroom.Enterprise;
using Volo.Abp.AutoMapper;

using CourseManagement = MicroClassroom.BackendAdminApp.Host.Pages.CourseManagement;
using TeacherManagement = MicroClassroom.BackendAdminApp.Host.Pages.TeacherManagement;
using BannerManagement = MicroClassroom.BackendAdminApp.Host.Pages.BannerManagement;
using MechanismManagement = MicroClassroom.BackendAdminApp.Host.Pages.MechanismManagement;

namespace MicroClassroom.BackendAdminApp.Host;

public class BackendAdminAppAutoMapperProfile : Profile
{
    public BackendAdminAppAutoMapperProfile()
    {
        // MechanismManagement
        CreateMap<MechanismManagement.NewModel.MechanismViewModel, CreateMechanismInput>()
            .Ignore(x => x.Image);
        CreateMap<MechanismDto, MechanismManagement.EditModel.MechanismViewModel>();
        CreateMap<MechanismManagement.EditModel.MechanismViewModel, UpdateMechanismInput>();
        CreateMap<MechanismDto, MechanismManagement.InfoModel.MechanismViewModel>();
        CreateMap<MechanismManagement.InfoModel.MechanismViewModel, UpdateMechanismInput>();

        // CourseManagement
        CreateMap<CourseManagement.NewModel.CourseViewModel, CreateCourseInput>()
            .Ignore(x => x.Image);
        CreateMap<CourseDto, CourseManagement.EditModel.CourseViewModel>();
        CreateMap<CourseManagement.EditModel.CourseViewModel, UpdateCourseInput>();

        // TeacherManagement
        CreateMap<TeacherManagement.NewModel.TeacherViewModel, CreateTeacherInput>()
            .Ignore(x => x.Image);
        CreateMap<TeacherDto, TeacherManagement.EditModel.TeacherViewModel>();
        CreateMap<TeacherManagement.EditModel.TeacherViewModel, UpdateTeacherInput>();

        // BannerManagement
        CreateMap<BannerManagement.NewModel.BannerViewModel, CreateBannerInput>()
            .Ignore(x => x.Image);
        CreateMap<BannerDto, BannerManagement.EditModel.BannerViewModel>();
        CreateMap<BannerManagement.EditModel.BannerViewModel, UpdateBannerInput>();
    }
}
