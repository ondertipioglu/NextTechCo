using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Shared.Exceptions
{
    public class NotFoundException : BaseException
    {
        internal NotFoundException(string typeName, int id) : base($"{typeName} with id '{id}' was not found")
        { }

        public static NotFoundException For<T>(int id)
        {
            return new NotFoundException(typeof(T).Name, id); 
        }
    }
}
