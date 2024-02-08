using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Exceptions
{
    internal class CustomerNotFoundException: ApplicationException
    {
        public CustomerNotFoundException()
        {
        }

        public CustomerNotFoundException(string message) : base(message)
        {

        }

        public CustomerNotFoundException(string message, Exception inner) : base(message, inner)

        {

        }
    }
}
