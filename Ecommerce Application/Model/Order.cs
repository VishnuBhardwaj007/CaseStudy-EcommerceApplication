using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Model
{
    internal class Order
    {
        private int orderId;
        private int customerId;
        private DateTime orderDate;
        private decimal totalPrice;
        private string shippingAddress;

        // Default Constructor
        public Order()
        {
        }

        // Parameterized Constructor
        public Order(int orderId, int customerId, DateTime orderDate, decimal totalPrice, string shippingAddress)
        {
            this.orderId = orderId;
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.totalPrice = totalPrice;
            this.shippingAddress = shippingAddress;
        }

        // Getters and Setters
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        public decimal TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public string ShippingAddress
        {
            get { return shippingAddress; }
            set { shippingAddress = value; }
        }
    }
}
