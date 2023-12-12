using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Elon.BasicProject.Application.Contracts.Users
{
    public interface IRoleAppService 
    {
        Task<UserDto> GetUserAsync(int id);
        Task<IEnumerable<UserDto>> GetUserByEmailAsync();
    }
}
