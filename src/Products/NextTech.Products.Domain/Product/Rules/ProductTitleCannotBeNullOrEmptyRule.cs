using NextTech.Products.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Domain
{
    public class ProductTitleCannotBeNullOrEmptyRule : IBusinessRule
    {
        private readonly string _title;
        public ProductTitleCannotBeNullOrEmptyRule(string title)
        {
            _title = title;
        }      

        public bool IsBroken() => string.IsNullOrEmpty(_title);
        
        public string Message => "ProductTitleCannotBeNullOrEmpty";
    }
}
