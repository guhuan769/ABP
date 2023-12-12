using Elon.BasicProject.Application.Contracts.Roles;
using Elon.BasicProject.Application.Contracts.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Elon.BasicProject.Application.Roles
{
    [Dependency(ServiceLifetime.Transient)] // 自动IOC注册
    public class RoleAppService : Contracts.Roles.IRoleAppService//, IRemoteService//,
        //ITransientDependency// 继承 ITransientDependency  可以不用注册AddTrans了
                            //,IApplicationService
    {
        public async Task<RoleDto> GetRoleAsync(int id)
        {
            await Task.CompletedTask;
            return new RoleDto()
            {
                Id = id,
                RoleName = "我想你了",
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
                    RoleName = "我想你了",
                    Email = "769540542@qq.com",
                    Password = "111111"
                }
            };
        }
    }
}
