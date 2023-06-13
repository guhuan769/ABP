using BasicProject.Application.Contracts.Roles;
using BasicProject.Application.Contracts.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace BasicProject.Application.Roles
{
   [Dependency(ServiceLifetime.Transient)] //自动IOC注册
    public class RoleAppService : IRoleAppService//,
        //IRemoteService//,//接口标记
                      // ITransientDependency //自动注册
    {
        public async Task<RoleDto> GetRoleAsync(int id)
        {
            await Task.CompletedTask;
            return new RoleDto()
            {
                Id = id,
                RoleName = "小龙",
                Email = "769540542@qq.com",
                Password = "111111"
            };
        }

        public async Task<IEnumerable<RoleDto>> GetRoleListAsync()
        {
            await Task.CompletedTask;
            return new List<RoleDto>()
            {
                new RoleDto()
                {
                    Id = 1,
                    RoleName = "小龙",
                    Email = "769540542@qq.com",
                    Password = "111111"
                }
            };
        }
    }
}
