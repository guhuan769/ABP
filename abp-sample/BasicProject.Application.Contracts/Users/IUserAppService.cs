using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicProject.Application.Contracts.Users
{
    public interface IUserAppService
    {
        Task<UserDto> GetUserAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersListAsync();
    }
}
