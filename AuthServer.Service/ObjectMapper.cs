using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AuthServer.Service
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() => 
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<DtoMapper>();
           });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
