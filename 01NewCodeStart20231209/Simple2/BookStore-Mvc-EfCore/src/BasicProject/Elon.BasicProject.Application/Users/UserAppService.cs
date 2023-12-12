using Elon.BasicProject.Application.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Elon.BasicProject.Application.Users
{
    public class UserAppService : IRoleAppService,IRemoteService,
        ITransientDependency// 继承 ITransientDependency  可以不用注册AddTrans了
                            //,IApplicationService
    {
        public async Task<UserDto> GetUserAsync(int id)
        {
            await Task.CompletedTask;
            return new UserDto()
            {
                Id = id,
                UserName = "李小龙",
                Email = "769540542@qq.com",
                Password = "111111"
            };
        }

        public async Task<IEnumerable<UserDto>> GetUserByEmailAsync()
        {
            await Task.CompletedTask;
            return new List<UserDto>()
            {
                new UserDto()
                {
                    Id = 1,
                    UserName = "李小龙",
                    Email = "769540542@qq.com",
                    Password = "111111"
                }
            };
        }
    }
}
