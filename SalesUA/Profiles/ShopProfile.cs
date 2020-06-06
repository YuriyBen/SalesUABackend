using AutoMapper;
using SalesUA.Entities;
using SalesUA.Helpers.ATB;
using SalesUA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesUA.Profiles
{
    public class ShopProfile:Profile
    {
        public ShopProfile()
        {
            CreateMap<Shop, ShopDTO>();
                //.ForMember(
                //dest => dest.Product,
                //opt => opt.MapFrom(src => src.Product.Parsing()));   
        }
    }
}
