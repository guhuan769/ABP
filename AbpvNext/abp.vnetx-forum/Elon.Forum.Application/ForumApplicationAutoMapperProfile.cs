using AutoMapper;
using Elon.Forum.Application.Contracts;
using Elon.Forum.Domain.Entities;
using Zhaoxi.Forum.Domain.Entities;

namespace Elon.Forum.Application;

public class ForumApplicationAutoMapperProfile : Profile
{
    public ForumApplicationAutoMapperProfile()
    {
        CreateMap<CategoryImportDto, CategoryEntity>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.No));

        CreateMap<TopicImportDto, TopicEntity>()
            .ForMember(dest => dest.TopicName, options => options.MapFrom(src => src.Title))
            .ForMember(dest => dest.TopicContent, options => options.MapFrom(src => src.Content));

        CreateMap<CategoryEntity, CategoryDto>();
        CreateMap<CategoryDto, CategoryEntity>();

        CreateMap<TopicDto, TopicEntity>();
        CreateMap<TopicEntity, TopicDto>();

    }
}