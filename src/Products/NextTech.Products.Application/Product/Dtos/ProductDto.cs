using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Product.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryMinumumStockQuantity { get; set; }
    }
}
