using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BookStore.Notices
{
    /// <summary>
    /// 自定义的应用服务层
    /// </summary>
    public class NoticeAppService : 
        //CrudAppService<
        //Notice,
        //NoticeDto,
        //Guid,
        //PagedAndSortedResultRequestDto,
        //CreateUpdateNoticeDto
        //>, 
        INoticeAppService,IRemoteService
    {

        public NoticeAppService(IRepository<Notice, Guid> repository) //: base(repository)
        {
            
        }

        public Task<NoticeDto> CreateAsync(CreateUpdateNoticeDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<NoticeDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<NoticeDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> postReadNotice(Guid noticeId)
        {
            //应有数据库更新
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            Console.WriteLine($"This is {this.GetType().Name} {MethodBase.GetCurrentMethod().Name}" +
                $"Invoke noticeId={noticeId}" +
                $"");
            Console.WriteLine("****************************");
            Console.WriteLine("****************************");
            await Task.CompletedTask;
            return true;

        }

        public Task<NoticeDto> UpdateAsync(Guid id, CreateUpdateNoticeDto input)
        {
            throw new NotImplementedException();
        }
    }
}
