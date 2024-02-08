using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Exceptions
{
    internal class ProductNotFoundException:ApplicationException
    {
        public ProductNotFoundException() : base("Product not found.")
        {
        }

        public ProductNotFoundException(int productId) : base($"Product with ID {productId} not found.")
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }

        public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
