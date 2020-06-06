using AutoMapper;
using SalesUA.Entities;
using SalesUA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesUA.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}
