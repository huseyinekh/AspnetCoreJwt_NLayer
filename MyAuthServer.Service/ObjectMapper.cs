using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAuthServer.Service
{
    public static class ObjectMapper
    {
        private static Lazy<IMapper> lazy= new Lazy<IMapper>(() =>
        {

            var config = new MapperConfiguration(_ =>
            {
                _.AddProfile<DtoMapper>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper { get { return lazy.Value; } } 

    }
}
