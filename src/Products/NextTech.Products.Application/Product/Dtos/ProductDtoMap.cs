using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Product.Dtos
{
    public class ProductDtoMap : Profile
    {
        public ProductDtoMap()
        {
            CreateMap<Domain.Product, ProductDto>().ReverseMap();
        }
    }
}
