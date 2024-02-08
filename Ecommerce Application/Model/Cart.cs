using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Model
{
    internal class Cart
    {
       public  int cartId ;
        public int customerId;
        public  int productId;
        private int quantity;

        // Default Constructor
        public Cart()
        {
        }

        // Parameterized Constructor
        public Cart(int cartId, int customerId, int productId, int cartQuantity)
        {
            this.cartId = CartId;
            this.customerId = customerId;
            this.productId = productId;
            quantity = cartQuantity;
        }

        // Getters and Setters
        public int CartId
        {
            get { return cartId; }
            set { cartId = value; }
        }

        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
