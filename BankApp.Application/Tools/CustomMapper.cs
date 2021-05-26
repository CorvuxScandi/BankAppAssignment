using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BankApp.Application.Tools
{
    public static class CustomMapper
    {
        public static T MapDTO<S, T>(S source) where T : class where S : class
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<S, T>(); });

            IMapper iMapper = config.CreateMapper();
            var destination = iMapper.Map<S, T>(source);

            return destination;
        }

        public static T ReveceMap<S, T>(S source) where T : class where S : class
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<S, T>().ReverseMap();
            });
            IMapper iMapper = config.CreateMapper();
            var destination = iMapper.Map<S, T>(source);

            return destination;
        }
    }
}