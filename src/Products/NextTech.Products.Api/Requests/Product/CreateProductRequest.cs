using MediatR;
using System;

namespace NextTech.Products.Api.Requests.Product
{
    public class CreateProductRequest 
    {       
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public bool Active { get; set; }
        public int Category { get; set; }

    }
}
