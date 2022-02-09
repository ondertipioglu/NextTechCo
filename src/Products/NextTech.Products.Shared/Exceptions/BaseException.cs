using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.Products.Shared.Exceptions
{
    public class BaseException : Exception
    {
        internal BaseException(string businessMessage) : base(businessMessage)
        {
        }
    }
}
