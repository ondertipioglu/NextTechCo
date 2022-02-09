using NextTech.Core.MediatR.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Application.Product.Commands
{   
    public class DisableProductCommand : ICommand
    {
        public int Id { get; }
        public string Comment { get; }
        private DisableProductCommand(int id, string comment)
        {
            Id = id;
            Comment = comment;
        }

        public static DisableProductCommand Create(int id, string comment)
        {
            return new DisableProductCommand(id, comment);
        }
    }
}
