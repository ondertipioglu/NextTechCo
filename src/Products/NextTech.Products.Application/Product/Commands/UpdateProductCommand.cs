using NextTech.Core.MediatR.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Product.Commands
{
    public class UpdateProductCommand : ICommand
    {
        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        private UpdateProductCommand(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public static UpdateProductCommand Create(int id, string title, string description)
        {
            return new UpdateProductCommand(id, title, description);
        }
    }
}
