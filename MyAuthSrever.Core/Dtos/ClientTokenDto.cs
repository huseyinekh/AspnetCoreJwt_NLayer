using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthSrever.Core.Dtos
{
    public class ClientTokenDto
    {
        public string AccessToken { get; set; } = null!;
        public string AccessTokenExpiration { get; set; } = null!;
    }
}
