using NextTech.Core.MediatR.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Commands
{
    public class CreateProductCommand : ICommand<int>
    {
        public string Title { get; }
        public string Description { get; }
        public int Stock { get; }
        public int? CategoryId { get;  }
        public bool Active { get; set; }
        private CreateProductCommand(string title, string description, int stock, bool active, int? categoryId)
        {
            Title = title;
            Description = description;
            Stock = stock;
            Active = active;
            CategoryId = categoryId;
        }

        public static CreateProductCommand Create(string title, string description, int stock, bool active, int? categoryId)
        {
            return new CreateProductCommand(title, description, stock, active, categoryId);
        }
    }
}