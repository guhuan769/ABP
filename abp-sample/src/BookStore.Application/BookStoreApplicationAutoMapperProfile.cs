using AutoMapper;
using BookStore.Notices;
using System.Net;

namespace BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
       
        CreateMap<Notice, NoticeDto>();
        CreateMap<CreateUpdateNoticeDto, Notice>();
    }
}
