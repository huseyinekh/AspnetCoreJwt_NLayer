﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthSrever.Core.Dtos
{
    public class ClientLoginDto
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
    }
}
