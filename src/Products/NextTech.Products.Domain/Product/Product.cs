using NextTech.Core.BaseObject;
using NextTech.Products.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Domain
{
    public  class Product : BusinessRule, ICreateAudit, IUpdateAudit
    {
        private Product() { }
        public int Id { get; private set; }//TODO : Guid mi kalsin, auto id mi gozden gecir.
        public DateTime CreatedOn { get; private set; }
        public DateTime? UpdatedOn { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Stock { get; private set; }
        public int? CategoryId { get; private set; }
        public Category Category { get; private set; }
        public bool Active { get; private set; }
        //public bool IsOutOfStock
        //{
        //    get => Stock <= 0;
        //}
        private Product(DateTime createdOn, string title, string description, int stock, bool active, int? categoryId)
        {
            Title = title;
            Description = description;
            Stock = stock;
            Active = active;
            CategoryId = categoryId;
            CreatedOn = createdOn;
        }

        public static Product Create(DateTime createdOn,string title, string description, int stock, bool active, int? categoryId = null)
        {
            CheckRule(new ProductTitleCannotBeNullOrEmptyRule(title));
            CheckRule(new ProductTitleHaveaMaximumCharacterLimitationOf200Rule(title));

            return new Product(createdOn,title, description, stock,active, categoryId);
        }

        public void ChangeTitleAndDescriptionInfo(string title, string description)
        {
            CheckRule(new ProductTitleCannotBeNullOrEmptyRule(title));
            CheckRule(new ProductTitleHaveaMaximumCharacterLimitationOf200Rule(title));

            Title= title;
            Description = description;
            UpdatedOn = DateTime.Now;
        }
        public void Enable()
        {
            if (Active) throw new InvalidOperationException($"{nameof(Product)} {Id} is already active!");

            Active = true;    
            UpdatedOn = DateTime.Now;
        }

        public void Disable()
        {
            if (!Active) throw new InvalidOperationException($"{nameof(Product)} {Id} is already passive!");

            Active = false;
            UpdatedOn = DateTime.Now;
        }
    }
}
