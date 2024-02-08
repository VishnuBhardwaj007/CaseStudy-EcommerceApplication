using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Model
{
    public  class Customer
    {
        private int customerId;
        private string customerName;
        private string customerEmail;
        private string password;

        // Default Constructor
        public Customer()
        {
        }

        // Parameterized Constructor
        public Customer(int customerId, string customerName, string customerEmail, string customerPassword)
        {
            this.customerId = customerId;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
            password = customerPassword;
        }

        // Getters and Setters
        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        public string CustomerEmail
        {
            get { return customerEmail; }
            set { customerEmail = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
