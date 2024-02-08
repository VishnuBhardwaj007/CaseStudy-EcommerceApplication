using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Model
{
    internal class OrderItem
    {
        private int orderItemId;
        private int orderId;
        private int productId;
        private int quantity;

        // Default Constructor
        public OrderItem()
        {
        }

        // Parameterized Constructor
        public OrderItem(int orderItemId, int orderId, int productId, int orderItemQuantity)
        {
            this.orderItemId = orderItemId;
            this.orderId = orderId;
            this.productId = productId;
            quantity = orderItemQuantity;
        }

        // Getters and Setters
        public int OrderItemId
        {
            get { return orderItemId; }
            set { orderItemId = value; }
        }

        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
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
