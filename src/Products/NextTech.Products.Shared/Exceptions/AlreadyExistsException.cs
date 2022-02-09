using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Shared.Exceptions
{
    public class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException(string typeName, Guid id) : base($"{typeName} with id '{id}' already exist")
        {

        }

        public static AlreadyExistsException For<T>(Guid id)
        {
            return new AlreadyExistsException(typeof(T).Name, id);
        }
    }
}
