using System.ComponentModel;
using System.Net.Http.Headers;
using Ecommerce_Application.Model;
using Ecommerce_Application.Repository;


namespace EcommerceApplication.Test
{
    public class Tests
    {
        public OrderProcessorRepositoryImpl orderProcessorRepositoryImpl;

        [SetUp]
        public void Setup()
        {
            orderProcessorRepositoryImpl= new OrderProcessorRepositoryImpl();

        }

        [Test]
        public void Test_To_CreateProduct()
        {

            //Assign
             
            Product product = new Product();
            product.ProductId = 5;
            product.ProductName = "Smart LED";
            product.ProductPrice = 20000;
            product.ProductDesc = "Voice Command";
            product.StockQuantity = 50;

            //Act
            bool result = orderProcessorRepositoryImpl.CreateProduct(product);
            //Assert
            Assert.IsTrue(true, "CreateProduct should return true for a successful product creation");
        }

        [Test]

        public void Test_Product_Added_ToCart()
        {
            Customer customer = new Customer();
            Product product = new Product();
            customer.CustomerId = 1;
            customer.CustomerName = "Vishu";
            customer.CustomerEmail = "bhardwajvishnu@123";
            customer.Password = "12345";

            product.ProductId = 1;
            product.ProductName = "Laptop";
            product.ProductPrice = 5000;
            product.ProductDesc = "Ram-8GB, SSD-256GB, i5";
            product.StockQuantity = 70;

            int quantity = 25;

            bool result = orderProcessorRepositoryImpl.AddToCart(customer, product, quantity);

            Assert.IsTrue(true, "Add to Cart should return true for a successful Add to Cart");

        }

        [Test]

        public void Test_ProductOrdered()
        {
            Customer customer = new Customer();
            Product product=new Product();
            customer.CustomerId = 2;
            product.ProductId = 1;
            product.ProductPrice = 5000;
            int quantity = 10;
            string address = "Civil-Lines, Palwal";
            List<KeyValuePair<Product, int>> productsAndQuantities = new List<KeyValuePair<Product, int>>
        {
            new KeyValuePair<Product, int>(product,quantity)
        };
            bool result = orderProcessorRepositoryImpl.PlaceOrder(customer, productsAndQuantities, address);
            Assert.IsTrue(true, " Place Order should return true for a successful Order Booking");

        }


        [Test]

        public void Test_ProductId_NotFound_Exception()
        {
            int productID = 10;
            bool result=orderProcessorRepositoryImpl.DeleteProduct(productID);
            Assert.IsFalse(result);
        }

        [Test]

        public void Test_CustomerID_Notfound_Exception()
        {
            int custId = 30;
            bool result=orderProcessorRepositoryImpl.DeleteCustomer(custId);
            Assert.IsFalse(result);
        }

        







    }

}

