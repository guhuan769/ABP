using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elon.BasicProject.Application.Contracts.Roles
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
