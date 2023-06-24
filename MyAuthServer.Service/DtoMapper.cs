using AutoMapper;
using MyAuthServer.Core.Models;
using MyAuthSrever.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServer.Service
{
       class DtoMapper:Profile
    {
        public DtoMapper() { 
        
            CreateMap<ProductDto,Product>().ReverseMap();
            CreateMap<UserAppDto,UserApp>().ReverseMap();
        
        }

    }
}
