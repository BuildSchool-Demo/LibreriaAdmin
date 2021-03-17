using AutoMapper;
using LibreriaAdmin.Models;
using LibreriaAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Mappings
{
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            CreateMap<Order, OrderViewModel.OrderSingleResult>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(x => x.OrderId, y => y.MapFrom(o => o.OrderId))
                .ReverseMap();

            CreateMap<Member, OrderViewModel.OrderSingleResult>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(x => x.MemberUserName, y => y.MapFrom(o => o.MemberUserName))
                .ReverseMap();
        }
    }
}
