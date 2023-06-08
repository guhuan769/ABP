using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BookStore.Notices
{
    public interface INoticeAppService
        :ICrudAppService<
        NoticeDto,//Defines CRUD methods
        Guid,//Primary key of the Notice entity
        PagedAndSortedResultRequestDto,//Used for paging/sorting
        CreateUpdateNoticeDto//Used to create/update a book
        >
    {
        Task<bool> postReadNotice(Guid noticeId);
    }
}
