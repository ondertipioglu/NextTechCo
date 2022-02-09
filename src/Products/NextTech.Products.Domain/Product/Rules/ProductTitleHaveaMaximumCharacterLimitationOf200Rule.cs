using NextTech.Products.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Domain
{
    internal class ProductTitleHaveaMaximumCharacterLimitationOf200Rule : IBusinessRule
    {
        private readonly string _title;
        public ProductTitleHaveaMaximumCharacterLimitationOf200Rule(string title)
        {
            _title = title;
        }
        public bool IsBroken() => _title.Length > 200 ? true : false;
        public string Message => "ProductTitleHaveaMaximumCharacterLimitationOf200Rule";


    }
}
