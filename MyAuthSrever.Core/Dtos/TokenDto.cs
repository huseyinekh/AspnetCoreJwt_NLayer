﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthSrever.Core.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public DateTime? AccesTokenExpiration { get; set; }
        public string? RefreshToken { get; set; }

    }
}
