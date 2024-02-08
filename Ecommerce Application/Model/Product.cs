using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Model
{
    public  class Product
    {
        private int productId;
        private string productName;
        private decimal productPrice;
        private string productDesc;
        private int stockQuantity;

        // Default Constructor
        public Product()
        {
        }

        // Parameterized Constructor
        public Product(int productId, string productName, decimal productPrice, string productDescription, int productStockQuantity)
        {
            this.productId = productId;
            this.productName = productName;
            this.productPrice = productPrice;
            this.productDesc = productDescription;
            stockQuantity = productStockQuantity;
        }

        // Getters and Setters
        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public decimal ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }

        public string ProductDesc
        {
            get { return productDesc; }
            set { productDesc = value; }
        }

        public int StockQuantity
        {
            get { return stockQuantity; }
            set { stockQuantity = value; }
        }
    }
}
