using Ecommerce_Application.Exceptions;
using Ecommerce_Application.Model;
using Ecommerce_Application.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Services
{
    internal class EcommerceService : OrderProcessorRepositoryImpl,IEcommerceService
    {
        readonly IOrderProcessorRepository _orderRepository;
        public EcommerceService()
        {
            _orderRepository = new OrderProcessorRepositoryImpl();
        }

       

        public void Createproduct()
        {
            try
            {
                Product product = new Product();

                Console.WriteLine("Enter Product Id::");
                product.ProductId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Name::");
                product.ProductName = Console.ReadLine();
                Console.WriteLine("Enter Product Price::");
                product.ProductPrice = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter Product Description::");
                product.ProductDesc = Console.ReadLine();
                Console.WriteLine("Enter Product Stock Quantity ::");
                product.StockQuantity = int.Parse(Console.ReadLine());




                bool createProductStatus = _orderRepository.CreateProduct(product);

                if (createProductStatus)
                {
                    Console.WriteLine("Product created successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to create product.");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }



        public void RegisterCustomer()
        {
            Customer customer = new Customer();

            Console.WriteLine("Enter Customer Id::");
            customer.CustomerId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Customer Name ::");
            customer.CustomerName = Console.ReadLine();
            Console.WriteLine("Enter Customer Email::");
            customer.CustomerEmail = Console.ReadLine();
            Console.WriteLine("Enter Customer Password::");
            customer.Password = Console.ReadLine();
            

            bool createCustomerStatus = _orderRepository.CreateCustomer(customer);

            if (createCustomerStatus)
            {
                Console.WriteLine("Customer created successfully.");
            }
            else
            {
                Console.WriteLine("Failed to create customer.");
            }
        }

        //-----------------------------------Login System------------------------------------------------------

        public void Login()
        {
            Console.WriteLine("If already Registered-->Login");
            Console.WriteLine("Enter your Email::");
            string email= Console.ReadLine();
            Console.WriteLine("Enter your Password");
            string password= Console.ReadLine();
            OrderProcessorRepositoryImpl orderProcessorRepository = new OrderProcessorRepositoryImpl();

            bool IsRegistered=orderProcessorRepository.Login(email, password);
            if (IsRegistered)
            {
                Console.WriteLine("Welcome, Explore our products and enjoy a seamless shopping experience!");
            }
            else
            {
                Console.WriteLine("Please Register First");
                Console.WriteLine("Please Provide Information for Registration Below::");
                
                RegisterCustomer();
            }

        }

       
        #region Old DeleteProduct
        //public void DeleteProduct()
        //{
        //    try
        //    {
        //        Console.WriteLine("Enter Product ID of Product To Delete");
        //        int productIdToDelete = int.Parse(Console.ReadLine());

        //        bool deletionResult = _orderRepository.DeleteProduct(productIdToDelete);

        //        if (deletionResult)
        //        {
        //            Console.WriteLine($"Product with ID {productIdToDelete} deleted successfully.");
        //        }
        //        //else
        //        //{
        //        //    Console.WriteLine($"Failed to delete product with ID {productIdToDelete}.");
        //        //}
        //    }
        //    catch (ProductNotFoundException PNFE) 
        //    {
        //     Console.WriteLine(PNFE.Message);
        //    }

        //}
        #endregion
        public void DeleteProduct()
        {
            try
            {
                Console.WriteLine("Enter Product ID of Product To Delete");
                int productIdToDelete = int.Parse(Console.ReadLine());

                bool deletionResult = _orderRepository.DeleteProduct(productIdToDelete);

                if (deletionResult)
                {
                    Console.WriteLine($"Product with ID {productIdToDelete} deleted successfully.");
                }
                else
                {
                    // If deletionResult is false, throw ProductNotFoundException
                    throw new ProductNotFoundException($"Product with ID {productIdToDelete} not found.");
                }
            }
            catch (ProductNotFoundException PNFE)
            {
                Console.WriteLine(PNFE.Message);
            }
        }


        
        #region Old Code Delete Customer
        //public void DeleteCustomer()
        //{
        //    Console.WriteLine("Enter Customer ID of Customer To Delete");
        //    int customerIdToDelete = int.Parse(Console.ReadLine());

        //    bool deletionResult = _orderRepository.DeleteProduct(customerIdToDelete);

        //    if (deletionResult)
        //    {
        //        Console.WriteLine($"Customer with ID {customerIdToDelete} deleted successfully.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Failed to delete Customer with ID {customerIdToDelete}.");
        //    }

        //}
        #endregion
        #region Code Delete Customer-2
        //public void DeleteCustomer()
        //{
        //    try
        //    {
        //        Console.WriteLine("Enter Customer ID of Customer To Delete");
        //        int customerIdToDelete = int.Parse(Console.ReadLine());

        //        bool deletionResult = _orderRepository.DeleteCustomer(customerIdToDelete);

        //        if (deletionResult)
        //        {
        //            Console.WriteLine($"Customer with ID {customerIdToDelete} deleted successfully.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error deleting customer: {ex.Message}");
        //    }
        //}
        #endregion
        public void DeleteCustomer()
        {
            try
            {
                Console.WriteLine("Enter Customer ID of Customer To Delete");
                int customerIdToDelete = int.Parse(Console.ReadLine());

                bool deletionResult = _orderRepository.DeleteCustomer(customerIdToDelete);

                if (deletionResult)
                {
                    Console.WriteLine($"Customer with ID {customerIdToDelete} deleted successfully.");
                }
                else
                {
                    // If deletionResult is false, throw CustomerNotFoundException
                    throw new CustomerNotFoundException($"Customer with ID {customerIdToDelete} not found.");
                }
            }
            catch (CustomerNotFoundException cnfe)
            {
                Console.WriteLine(cnfe.Message);
            }
            
        }


       

        public void AddToCart()
        {
               OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
               Console.WriteLine("Products Avaialable::");
               List<Product> pdt = new List<Product>();
               pdt = GetAllFromProducts();
               foreach(Product p in pdt)
            {
                Console.WriteLine($"Product Id::{p.ProductId} || Product Name::{p.ProductName} || Product Price::{p.ProductPrice} || Product Description ::{p.ProductDesc} || Product Stock Quantity::{p.StockQuantity} ");
            }
            
                Customer customer = new Customer();
                Product product = new Product();

                Console.WriteLine("Enter Your Customer Id::");
                customer.CustomerId = int.Parse(Console.ReadLine());
                

                Console.WriteLine("Enter Product Id::");
                product.ProductId = int.Parse(Console.ReadLine());
                

                Console.WriteLine("Enter Quantity of Product");
                int quantity=int.Parse(Console.ReadLine());
           
                bool createProductStatus = _orderRepository.AddToCart(customer, product, quantity);
                decimal totalAmount = orderProcessorRepositoryImpl.GetTotalPriceInCart(customer.CustomerId);
                int stockQuant = GetStockQuantityForProduct(product.ProductId);
                 if (createProductStatus && quantity<= stockQuant )
                    {
                        Console.WriteLine("Product added to cart successfully.");
                        //totalAmount+=GetProductPrice(product.ProductId)*quantity;
                        Console.WriteLine($"Net Payable Amount::{totalAmount}");
                    }
                 else
                    {
                        Console.WriteLine("Failed to add product to cart.");
                    }
                
        }

        public void RemoveFromCart()
        {
            Customer customer= new Customer();
            Product product= new Product();
            Console.WriteLine("Enter Your Customer ID::");
            customer.CustomerId=int.Parse(Console.ReadLine()) ;
            Console.WriteLine("Enter Product ID You Want to Remove From Cart::");
            product.ProductId=int.Parse(Console.ReadLine()) ;

            bool deleteStatus=_orderRepository.RemoveFromCart(customer, product);
            if (deleteStatus)
            {
                Console.WriteLine($"Your product has been deleted Successfully from Cart!");
            }
            else
            {
                Console.WriteLine("Failed in Deleting From Cart");
            }
                
        }




        public void GetAllFromCart()
        {
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
            List<string> product = new List<string>();
            Customer customer= new Customer();
            Console.WriteLine("\t");
            
            Console.WriteLine("Enter Customer ID::");
            customer.CustomerId= int.Parse(Console.ReadLine());
            
            Console.WriteLine("\t");
            Console.WriteLine("=>Your Products in Cart are::");
            Console.WriteLine("\t");

            product =_orderRepository.GetAllFromCart(customer);
            decimal totalAmount = orderProcessorRepositoryImpl.GetTotalPriceInCart(customer.CustomerId);

            int i = 1;

            foreach (string item in product)
            {
                Console.WriteLine($"Product {i}=>{item}");
                i++;
                Console.WriteLine("\t");
            }

            Console.WriteLine("\t");
            Console.WriteLine($"Net Payable Amount::{totalAmount}");



        }



        //------------------------Get Orders By Customer ID--------------------------------------------
        #region   Old Code for GetOrdersByCustomer
        //public void GetOrdersByCustomer()
        //{
        //    Console.WriteLine("Enter Your Customer ID ");
        //    int custId= int.Parse(Console.ReadLine());

        //    List<KeyValuePair<string, int>> orders = _orderRepository.GetOrdersByCustomer(custId);
        //    int i = 0;

        //    foreach (var order in orders)
        //    {
        //        Console.WriteLine($"Product{i}: {order.Key}  ||  Quantity: {order.Value}");
        //        i++;
        //    }
        //}
        #endregion

        public void GetOrdersByCustomer()
        {
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl= new OrderProcessorRepositoryImpl();
            try
            {
                Console.WriteLine("Enter Your Customer ID ");
                int custId = int.Parse(Console.ReadLine());

                List<KeyValuePair<string, int>> orders = _orderRepository.GetOrdersByCustomer(custId);
                decimal totalAmount = orderProcessorRepositoryImpl.GetTotalPriceOfOrdersByCustomer(custId);

                if (orders.Count == 0)
                {
                    throw new OrderNotFoundException($"No orders found for customer with ID {custId}.");
                }

                int i = 1;

                foreach (var order in orders)
                {
                    Console.WriteLine($"Product{i}: {order.Key}  ||  Quantity: {order.Value}");
                    i++;
                }
                Console.WriteLine($"Total Bill::{totalAmount}");
            }
            
            catch (OrderNotFoundException onfe)
            {
                Console.WriteLine(onfe.Message);
            }
           
        }



        //-----------------------------------------------------------------------------------------------

        public void PlaceOrder()
        {
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();

            Customer customer = new Customer();
            Console.WriteLine("Enter Your Customer ID::");
            customer.CustomerId = int.Parse(Console.ReadLine());

            Console.WriteLine("Products Avaialable::");
            List<Product> pdt = new List<Product>();
            pdt = GetAllFromProducts();
            foreach (Product p in pdt)
            {
                Console.WriteLine($"Product Id::{p.ProductId} | Product Name::{p.ProductName} | Product Price::{p.ProductPrice} | Product Description ::{p.ProductDesc} | Product Stock Quantity::{p.StockQuantity} ");
            }
            Product product = new Product();
            Console.WriteLine("Enter Product ID::");
            product.ProductId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Product Price::");
            product.ProductPrice = int.Parse(Console.ReadLine());
            
            Console.WriteLine("\t");
            Console.WriteLine("Enter Product Quantity::");
            int quantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Your Shipping Address");
            string Address = Console.ReadLine();
            List<KeyValuePair<Product, int>> productsAndQuantities = new List<KeyValuePair<Product, int>>
        {
            new KeyValuePair<Product, int>(product,quantity )
        };
            int stockQuant = orderProcessorRepositoryImpl.GetStockQuantity(product.ProductId);

            bool placeOrderStatus = _orderRepository.PlaceOrder(customer, productsAndQuantities, Address);
            decimal totalAmount = orderProcessorRepositoryImpl.GetTotalPriceOfOrdersByCustomer(customer.CustomerId);

            if (placeOrderStatus  && stockQuant>=quantity)
            {
                Console.WriteLine("Your Order Placed Successfully");
                Console.WriteLine($"Total Bill Of Current Purchase::{product.ProductPrice*quantity}");
                Console.WriteLine($"Net Payable Amount:{totalAmount}");
                orderProcessorRepositoryImpl.UpdateStockQuantity(product.ProductId, quantity);

            }

            else
            {
                Console.WriteLine("Failed to Place Order");
            }


        }


      


    }
    }


    #region Dlt Product code 1
    //void DeleteProduct()
    //{
    //    Console.WriteLine("Enter Product ID of Product To Delete");
    //    int productIdToDelete=int.Parse(Console.ReadLine());

    //    bool deletionResult = _orderRepository.DeleteProduct(productIdToDelete);

    //    if (deletionResult)
    //    {
    //        Console.WriteLine($"Product with ID {productIdToDelete} deleted successfully.");
    //    }
    //    else
    //    {
    //        Console.WriteLine($"Failed to delete product with ID {productIdToDelete}.");
    //    }
    //}
    #endregion






