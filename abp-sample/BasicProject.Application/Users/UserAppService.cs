using BasicProject.Application.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace BasicProject.Application.Users
{
    public class UserAppService : IUserAppService,
        IRemoteService,//接口标记
        ITransientDependency //自动注册
    {
        public async Task<UserDto> GetUserAsync(int id)
        {
            await Task.CompletedTask;
            return new UserDto()
            {
                Id = id,
                UserName = "小龙",
                Email = "769540542@qq.com",
                Password = "111111"
            };
        }

        public async Task<IEnumerable<UserDto>> GetUsersListAsync()
        {
            await Task.CompletedTask;
            return new List<UserDto>()
            {
                new UserDto()
                {
                    Id = 1,
                    UserName = "小龙",
                    Email = "769540542@qq.com",
                    Password = "111111"
                }
            };
        }
    }
}
