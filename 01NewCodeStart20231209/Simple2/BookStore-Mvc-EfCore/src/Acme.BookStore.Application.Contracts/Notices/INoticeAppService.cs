﻿using Acme.BookStore.Books;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Notices
{
    public interface INoticeAppService 
        //: ICrudAppService< //Defines CRUD methods
        //NoticeDto, //Used to show books
        //Guid, //Primary key of the Notice entity
        //PagedAndSortedResultRequestDto, //Used for paging/sorting
        //CreateUpdateNoticeDto>
    {
        Task<bool> PostReadNotice(Guid noticeId);
    }
}
