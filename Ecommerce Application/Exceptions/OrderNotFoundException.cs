using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Exceptions
{
    internal class OrderNotFoundException:ApplicationException
    {
        public OrderNotFoundException()
        {
        }

        public OrderNotFoundException(string message)
            : base(message)
        {
        }

        public OrderNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
