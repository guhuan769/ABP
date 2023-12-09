using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Notices
{
    /// <summary>
    /// 自定义的应用服务层
    /// </summary>
    public class NoticeAppService :
    //: CrudAppService<
    //   Notice,
    //   NoticeDto,
    //   Guid,
    //   PagedAndSortedResultRequestDto,
    //   CreateUpdateNoticeDto>, 
    INoticeAppService, IRemoteService, ITransientDependency
    {
        public NoticeAppService(IRepository<Notice, Guid> repository) //: base(repository)
        {
        }


        public async Task<bool> PostReadNotice(Guid noticeId)
        {
            //应有数据库更新
            Console.WriteLine("********************************");
            Console.WriteLine("********************************");
            Console.WriteLine($"This is {this.GetType().Name} {MethodInfo.GetCurrentMethod().Name}  Invoke noticeId={noticeId}");
            Console.WriteLine("********************************");
            Console.WriteLine("********************************");
            await Task.CompletedTask;
            return true;
        }
    }
}
