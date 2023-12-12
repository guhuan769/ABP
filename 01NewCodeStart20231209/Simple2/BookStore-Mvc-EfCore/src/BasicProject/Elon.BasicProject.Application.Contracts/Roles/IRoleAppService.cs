using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Elon.BasicProject.Application.Contracts.Roles
{
    public interface IRoleAppService : IRemoteService
    {
        Task<RoleDto> GetRoleAsync(int id);
        Task<IEnumerable<RoleDto>> GetRoleListAsync();
    }
}
