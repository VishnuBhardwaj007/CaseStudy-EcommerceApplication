using Ecommerce_Application.Model;
using Ecommerce_Application.Repository;
using Ecommerce_Application.Services;

namespace Ecommerce_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            IEcommerceService service = new EcommerceService();



           
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register Customer");

            int custInput=int.Parse(Console.ReadLine());
            switch (custInput)
            {
                case 1:
                    service.Login(); 
                    break;
                case 2:
                    Console.WriteLine("Welcome, Please Provide Information For Registeration Below ::");
                    service.RegisterCustomer();
                    break;
            }

            #region Test-1
            //IOrderProcessorRepository orderProcessorRepository = new OrderProcessorRepositoryImpl();

            //int productIdToDelete = 4;

            //// Test the DeleteProduct method
            //bool deletionResult = orderProcessorRepository.DeleteProduct(productIdToDelete);

            //if (deletionResult)
            //{
            //    Console.WriteLine($"Product with ID {productIdToDelete} deleted successfully.");
            //}
            //else
            //{
            //    Console.WriteLine($"Failed to delete product with ID {productIdToDelete}.");
            //}

            #endregion

            while (true)
            {
                
                Console.WriteLine("Choose an operation:");
                //Console.WriteLine("1. Register Customer");
                Console.WriteLine("1. Create Product");
                Console.WriteLine("2. Delete Product");
                Console.WriteLine("3. Add to Cart");
                Console.WriteLine("4. View Cart");
                Console.WriteLine("5. Place Order");
                Console.WriteLine("6. View Customer Order");
                Console.WriteLine("7. To Log Out");

                string userInput = Console.ReadLine();
                
                Console.Clear();
                switch (userInput)
                {
                    //case "1":
                    //    // Register Customer
                    //    Console.WriteLine("Welcome, Please Provide Information For Registeration Below ::");
                    //    service.RegisterCustomer();
                    //    break;
                    case "1":
                        // Create Product
                        service.Createproduct();
                        break;
                    case "2":
                        // Delete Product
                        service.DeleteProduct();
                        break;
                    case "3":
                        // Add to Cart
                        service.AddToCart();
                        break;
                    case "4":
                        // View Cart
                        service.GetAllFromCart();
                        break;
                    case "5":
                        // Place Order
                        service.PlaceOrder();
                        break;
                    case "6":
                        // View Customer Order
                        service.GetOrdersByCustomer();
                        break;
                    case "7":
                        // Exit the application
                        Console.WriteLine("Exiting From Ecommerce Application....");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid option.");
                        break;
                }
                #region CreateCustomer
                //}
                //Create a customer
                //Customer customer = new Customer
                //{
                //    CustomerId = 4,
                //    CustomerName = "John D",
                //    CustomerEmail = "john@ex.com",
                //    Password = "password1"
                //};


                //// Call the CreateCustomer method
                //bool createCustomerStatus = orderProcessorRepository.CreateCustomer(customer);

                //if (createCustomerStatus)
                //{
                //    Console.WriteLine("Customer created successfully.");
                //}
                //else
                //{
                //    Console.WriteLine("Failed to create customer.");
                //}

                // Call the CreateProduct method
                #endregion

                #region CreateProduct
                //Product product = new Product
                //{
                //    ProductId = 4,
                //    ProductName = "Laptop",
                //    ProductPrice = 999.99m,
                //    ProductDesc = "High-performance laptop",
                //    StockQuantity = 10
                //};
                //bool createProductStatus = orderProcessorRepository.CreateProduct(product);

                //if (createProductStatus)
                //{
                //    Console.WriteLine("Product created successfully.");
                //}
                //else
                //{
                //    Console.WriteLine("Failed to create product.");
                //}
                #endregion
                #region test-2
                //OrderProcessorRepositoryImpl  orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();


                //List<Customer> cst=new List<Customer>();
                //cst=orderProcessorRepositoryImpl.GetAllCustomers();

                //foreach(Customer customer in cst)
                //{
                //    Console.WriteLine($"Id is :{customer.CustomerId}");
                //    Console.WriteLine($"Id is :{customer.CustomerName}");
                //    Console.WriteLine($"Id is :{customer.CustomerEmail}");
                //    Console.WriteLine($"Id is :{customer.Password}");

                //}
                //Customer customer = new Customer();
                //Console.WriteLine("Enter CustomerId::");
                //customer.CustomerId = int.Parse(Console.ReadLine());
                //Console.WriteLine("Enter Customer Name::");
                //customer.CustomerName = Console.ReadLine();
                //Console.WriteLine("Enter CustomerEmail::");
                //customer.CustomerEmail = Console.ReadLine();
                //Console.WriteLine("Enter Customer Password::");
                //customer.Password = Console.ReadLine();
                #endregion

            }


        }

    }
}

