using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthSrever.Core.Dtos
{
    public class CreateUserDto
    {
        public string? UserName { get; set; } 
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
