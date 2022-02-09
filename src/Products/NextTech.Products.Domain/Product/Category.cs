using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Domain
{
    public class Category
    {
        private Category() { }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int MinumumStockQuantity { get; private set; }

        public virtual IEnumerable<Product> Products { get; private set; }

        private Category(string name, int minumumStockQuantity)
        {        
            Name = name;
            MinumumStockQuantity = minumumStockQuantity;
        }
        public static Category Create(string name, int minumumStockQuantity)
        {
            //Guard
            return new Category(name, minumumStockQuantity);
        }
    }
}
