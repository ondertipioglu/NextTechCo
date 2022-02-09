using MediatR;
using NextTech.Core.MediatR.Queries;
using NextTech.Products.Application.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Product.Queries
{
    public class GetProductByIdQuery : IQuery<ProductDto>
    {
        public int Id { get; }

        private GetProductByIdQuery(int id)
        {
            Id = id;
        }

        public static GetProductByIdQuery Create(int id)
        {
            return new GetProductByIdQuery(id);
        }
    }
}
