using Ecommerce_Application.Model;
using Ecommerce_Application.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Repository
{
    public class OrderProcessorRepositoryImpl : IOrderProcessorRepository
    {
        public string connectionString;
        //sql connection class
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public OrderProcessorRepositoryImpl()
        {
            connectionString = DBConnection.GetConnectionString();
            cmd = new SqlCommand();
        }


       
        

        

        //------------------------------------CREATE PRODUCT--------------------------------------------

        public bool CreateProduct(Product product)
        {



            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {

                    cmd.CommandText = "insert into Product values(@productId,@productName,@productPrice,@productDesc,@stockQuantity)";
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);
                    cmd.Parameters.AddWithValue("@productName", product.ProductName);
                    cmd.Parameters.AddWithValue("@productPrice", product.ProductPrice);
                    cmd.Parameters.AddWithValue("@productDesc", product.ProductDesc);
                    cmd.Parameters.AddWithValue("@stockQuantity", product.StockQuantity);
                    
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    int addProductStatus = cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    return addProductStatus > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating product: {ex.Message}");
                return false;
            }


        }

        
        //------------------------------CREATE CUSTOMER-------------------------------------------------
        public bool CreateCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {

                    cmd.CommandText = "insert into Customer values(@customerId,@customerName,@customerEmail,@password)";
                    cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@customerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@customerEmail", customer.CustomerEmail);
                    cmd.Parameters.AddWithValue("@password", customer.Password);

                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    int addCustomerStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return addCustomerStatus > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating customer: {ex.Message}");
                return false;
            }




        }

        //---------------------------------------Login System--------------------------------------------------
        public bool Login(string email, string password)
        {
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM Customer WHERE customerEmail = @customerEmail AND password = @password";
                        cmd.Connection = sqlconnection;

                        cmd.Parameters.AddWithValue("@customerEmail", email);
                        cmd.Parameters.AddWithValue("@password", password);

                        sqlconnection.Open();

                        // ExecuteScalar returning the count of matching records
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                return false;
            }
        }



        //----------------------------------DELETE PRODUCT------------------------------------
        public bool DeleteProduct(int ProductId)
        {
            #region old code
            //try
            //{
            //    using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            //    {

            //        cmd.CommandText = @"
            //    DELETE FROM cart WHERE productId = @productId;
            //    DELETE FROM orders WHERE orderId IN (SELECT oi.orderId FROM order_item oi WHERE productId = @productId);
            //    DELETE FROM order_item WHERE productId = @productId;
            //    DELETE FROM product WHERE productId = @productId;
            //";




            //        cmd.Parameters.AddWithValue("@productId", ProductId);


            //        cmd.Connection = sqlconnection;
            //        sqlconnection.Open();
            //        int addCustomerStatus = cmd.ExecuteNonQuery();

            //        return addCustomerStatus > 0;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error Deleting Product: {ex.Message}");
            //    return false;
            //}
            #endregion
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                   

                    // Delete from cart
                    cmd.CommandText = "DELETE FROM cart WHERE productId = @productId";
                    cmd.Parameters.AddWithValue("@productId", ProductId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    int rowsAffectedCart = cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM order_item WHERE productId = @productId";
                    int rowsAffectedOrderItems = cmd.ExecuteNonQuery();

                    // Delete from orders
                    cmd.CommandText = "DELETE FROM orders WHERE orderId IN (SELECT orderId FROM order_item WHERE productId = @productId)";
                    int rowsAffectedOrders = cmd.ExecuteNonQuery();

                    

                    // Delete from products
                    cmd.CommandText = "DELETE FROM product WHERE productId = @productId";
                    int rowsAffectedProducts = cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                    return (rowsAffectedCart + rowsAffectedOrders + rowsAffectedOrderItems + rowsAffectedProducts) > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
                return false;
            }
        }

        //-------------------------------------DELETE Customer--------------------------------------------
        public bool DeleteCustomer(int CustomerId)
        {
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {

                    cmd.CommandText = "DELETE FROM cart where customerId=@customerId";
                    cmd.Parameters.AddWithValue("@customerId", CustomerId);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    int rowsAffectedCart = cmd.ExecuteNonQuery();

                    cmd.CommandText = "DELETE FROM order_item WHERE orderId IN (SELECT orderId FROM orders WHERE customerId = 1)";
                    int rowsAffectedOrderItem = cmd.ExecuteNonQuery();

                    cmd.CommandText = "Delete from orders where customerId=@customerId";
                    int rowsAffectedOrders = cmd.ExecuteNonQuery();

                    cmd.CommandText = "Delete from customer where customerId=@customerId";
                    int rowsAffectedCustomer = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return (rowsAffectedCart + rowsAffectedOrders + rowsAffectedOrderItem + rowsAffectedCustomer) > 0;
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Deleting customer: {ex.Message}");
                return false;
            }

        }
        //-----------------------------------ADD TO CART------------------------------------

        public bool AddToCart(Customer customer, Product product, int quantity)
        {
           
            try
            {
                
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {

                    cmd.CommandText = "insert into cart values(@customerId,@productId,@quantity)";
                    

                    cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);
                    
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    int addProductStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return addProductStatus > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in adding to cart: {ex.Message}");
                return false;
            }
            #region code 1
            //    try
            //    {
            //        using (SqlConnection sqlconnection = new SqlConnection(connectionString))
            //        {
            //            // Check if the customer and product exist
            //            Customer foundCustomer = cst.Find(c => c.CustomerId == customer.CustomerId);
            //            Product foundProduct = pdt.Find(p => p.ProductId == product.ProductId);

            //            if (foundCustomer == null || foundProduct == null)
            //            {
            //                Console.WriteLine("Customer or product not found.");
            //                return false;
            //            }

            //            cmd.CommandText = "insert into cart (customerId, productId, quantity) values (@customerId, @productId, @quantity)";
            //            cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
            //            cmd.Parameters.AddWithValue("@productId", product.ProductId);
            //            cmd.Parameters.AddWithValue("@quantity", quantity);

            //            cmd.Connection = sqlconnection;
            //            sqlconnection.Open();
            //            int addProductStatus = cmd.ExecuteNonQuery();
            //            return addProductStatus > 0;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Error in adding to cart: {ex.Message}");
            //        return false;
            //    }
#endregion
        }

        

     
       
        #region Get All From Product
        public List<Product> GetAllFromProducts()
        {
            List<Product> productList = new List<Product>();
            try
            {
                using (SqlConnection sqlconn = new SqlConnection(connectionString))
                {
                    sqlconn.Open();  

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "select * from product";
                        cmd.Connection = sqlconn;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.ProductId = (int)reader["productId"];
                            product.ProductName = (string)reader["productName"];
                            product.ProductPrice = (decimal)reader["productPrice"];  // Assuming productPrice is decimal
                            product.ProductDesc = (string)reader["productDesc"];
                            product.StockQuantity = (int)reader["stockQuantity"];
                            productList.Add(product);
                        }
                    }  // Dispose of the SqlCommand

                    sqlconn.Close();  // Close the connection
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productList;
        }
        #endregion

        //------------------------------------------------------------------------------------------------------


        public bool RemoveFromCart(Customer customer, Product product)
        {
            try
            {
                using (SqlConnection sqlconnection=new SqlConnection(connectionString))
                {
                    cmd.CommandText = "Delete from cart where customerId=@customerId and productId=@productId";
                    cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);

                    cmd.Connection = sqlconnection;
                    sqlconnection.Open();
                    int addProductStatus = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return addProductStatus > 0;

                }
            }
            catch(Exception Ex) 
            {  
                Console.WriteLine(Ex.Message);
                return false; 
            }
        }
        //-------------------------------------------------------------------------------------
        #region Code-1
        //public List<string> GetAllFromCart(Customer customer)
        //{
        //    List<string> productNames = new List<string>();
        //    try
        //    {
        //        using (SqlConnection sqlconnection = new SqlConnection(connectionString))
        //        {

        //            cmd.CommandText = @"
        //               SELECT p.productName
        //               FROM Product p
        //               JOIN Cart c ON c.productId = p.productId
        //               WHERE c.customerId = @customerId";

        //            cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);

        //            cmd.Connection = sqlconnection;
        //            sqlconnection.Open();

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                string productName = reader["productName"].ToString();
        //                productNames.Add(productName);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error retrieving product names: {ex.Message}");
        //    }

        //    return productNames;
        //}
        #endregion
        #region Code-2
        //public List<string> GetAllFromCart(Customer customer)
        //{
        //    List<string> productNames = new List<string>();
        //    try
        //    {
        //        using (SqlConnection sqlconnection = new SqlConnection(connectionString))
        //        {
        //            cmd.CommandText = @"
        //        SELECT p.productName
        //        FROM Product p
        //        JOIN Cart c ON c.productId = p.productId
        //        WHERE c.customerId = @customerId";

        //            cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);

        //            cmd.Connection = sqlconnection;
        //            sqlconnection.Open();

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                string productName = reader["productName"].ToString();
        //                productNames.Add(productName);
        //            }

        //            // Clear parameters after executing the query
        //            cmd.Parameters.Clear();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error retrieving product names: {ex.Message}");
        //    }

        //    return productNames;
        //}
        #endregion

        public List<string> GetAllFromCart(Customer customer)
        {
            List<string> productNames = new List<string>();
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = @"
                                          SELECT p.productName
                                          FROM Product p
                                          JOIN Cart c ON c.productId = p.productId
                                          WHERE c.customerId = @customerId";

                        cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);

                        cmd.Connection = sqlconnection;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string productName = reader["productName"].ToString();
                                productNames.Add(productName);
                            }
                        }  // Dispose of the SqlDataReader
                    }  // Dispose of the SqlCommand
                }  // Dispose of the SqlConnection
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product names: {ex.Message}");
            }

            return productNames;
        }



        //------------------------------PLACE ORDER------------------------------------------------
        #region old code
        //public bool PlaceOrder(Customer customer, List<KeyValuePair<Product, int>> productsAndQuantities, string shippingAddress)
        //{
        //    try
        //    {
        //        using (SqlConnection sqlconnection = new SqlConnection(connectionString))
        //        {


        //            //Insert into Orders table
        //            cmd.CommandText = "INSERT INTO Orders (customerId, orderDate, totalPrice, shippingAddress) VALUES (@customerId, @orderDate, @totalPrice, @shippingAddress); SELECT SCOPE_IDENTITY();";
        //            cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
        //            cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);

        //            // Calculate total price = products * quantities
        //            decimal totalPrice = CalculateTotalPrice(productsAndQuantities);
        //            cmd.Parameters.AddWithValue("@totalPrice", totalPrice);

        //            cmd.Parameters.AddWithValue("@shippingAddress", shippingAddress);

        //            // Execute scalar to get the newly inserted orderId
        //            int orderId = Convert.ToInt32(cmd.ExecuteScalar());

        //            // Step 2: Insert into order_items table
        //            foreach (var productQuantityPair in productsAndQuantities)
        //            {
        //                cmd.CommandText = "INSERT INTO order_item (orderId, productId, quantity) VALUES (@orderId, @productId, @quantity)";

        //                cmd.Parameters.AddWithValue("@orderId", orderId);
        //                cmd.Parameters.AddWithValue("@productId", productQuantityPair.Key.ProductId);
        //                cmd.Parameters.AddWithValue("@quantity", productQuantityPair.Value);
        //                cmd.Parameters.Clear();
        //                sqlconnection.Open();
        //                cmd.ExecuteNonQuery();
        //            }

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error placing order: {ex.Message}");
        //        return false;
        //    }
        //}
        #endregion
        public bool PlaceOrder(Customer customer, List<KeyValuePair<Product, int>> productsAndQuantities, string shippingAddress)
        {
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();

                   
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlconnection;
                        cmd.CommandText = "INSERT INTO Orders (customerId, orderDate, totalPrice, shippingAddress) VALUES (@customerId, @orderDate, @totalPrice, @shippingAddress); SELECT SCOPE_IDENTITY();";
                        cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);

                        // total price = products * quantities
                        decimal totalPrice = CalculateTotalPrice(productsAndQuantities);
                        cmd.Parameters.AddWithValue("@totalPrice", totalPrice);

                        cmd.Parameters.AddWithValue("@shippingAddress", shippingAddress);

                        // Execute scalar to get the newly inserted orderId
                        int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                        // Inserting into order_items table
                        cmd.CommandText = "INSERT INTO order_item (orderId, productId, quantity) VALUES (@orderId, @productId, @quantity)";

                        foreach (var productQuantityPair in productsAndQuantities)
                        {
                            cmd.Parameters.Clear(); 
                            cmd.Parameters.AddWithValue("@orderId", orderId);
                            cmd.Parameters.AddWithValue("@productId", productQuantityPair.Key.ProductId);
                            cmd.Parameters.AddWithValue("@quantity", productQuantityPair.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error placing order: {ex.Message}");
                return false;
            }
        }

        private decimal CalculateTotalPrice(List<KeyValuePair<Product, int>> productsAndQuantities)
        {
         decimal totalPrice = 0;

        foreach (var productQuantityPair in productsAndQuantities)
        {
        totalPrice += productQuantityPair.Key.ProductPrice * productQuantityPair.Value;
        }

        return totalPrice;
        }

        #region GetStockQuantity
        public int GetStockQuantity(int productID)
        {
            try
            {
                using (SqlConnection sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();

                  
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = sqlconnection;
                        cmd.CommandText = "SELECT stockQuantity FROM product WHERE productId = @productId";
                        cmd.Parameters.AddWithValue("@productId", productID);

                        
                        int result = (int)cmd.ExecuteScalar();

                        
                        if (result != null )
                        {
                            return (result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in getting stock quantity: {ex.Message}");
            }

            // default value in case of an error
            return 0;
        }
        #endregion


        //----------------------------------------------------------------------------------------------

        public List<KeyValuePair<string, int>> GetOrdersByCustomer(int customerId)
        {
            List<KeyValuePair<string, int>> orderDetails = new List<KeyValuePair<string, int>>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                  

                    
                        cmd.CommandText = "SELECT p.productName, oi.quantity FROM product p JOIN order_item oi ON p.productId = oi.productId JOIN Orders o ON o.orderId = oi.orderId WHERE o.customerId = @customerId";
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Connection = sqlConnection;
                        sqlConnection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string productName = reader["productName"].ToString();
                            int quantity = (int)reader["quantity"];

                            orderDetails.Add(new KeyValuePair<string, int>(productName, quantity));
                        }
                        cmd.Parameters.Clear();
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting orders by customer: {ex.Message}");
            }

            return orderDetails;
        }

        //------------------------Get Stock Quantity For Product---------------------------------------------
        public int GetStockQuantityForProduct(int productId)
        {
            int stockQuantity = 0;

            try
            {
                using (SqlConnection sqlconn = new SqlConnection(connectionString))
                {
                    sqlconn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT StockQuantity FROM Product WHERE ProductId = @ProductId";
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Connection = sqlconn;

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            stockQuantity = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return stockQuantity;
        }

        #region code--1

        //public int GetProductPrice(int customerId,int productId)
        //{
        //    int productPrice = 0;

        //    try
        //    {
        //        using (SqlConnection sqlconn = new SqlConnection(connectionString))
        //        {
        //            sqlconn.Open();

        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                // cmd.CommandText="select productPrice from product p join orders 
        //                cmd.CommandText = "SELECT productPrice From product p join cart c on p.productId=c.productId where c.customerId=@customerId";
        //                cmd.Parameters.AddWithValue("@ProductId", productId);
        //                cmd.Parameters.AddWithValue("@ProductId", productId);
        //                cmd.Connection = sqlconn;

        //                object result = cmd.ExecuteScalar();

        //                if (result != null && result != DBNull.Value)
        //                {
        //                    productPrice = Convert.ToInt32(result);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return productPrice;
        //}
        #endregion
        //-------------------------------------Get Total Price In cart------------------------------------------
        public decimal GetTotalPriceInCart(int customerId)
        {
            decimal totalPrice = 0;

            try
            {
                using (SqlConnection sqlconn = new SqlConnection(connectionString))
                {
                    sqlconn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = @"SELECT SUM(p.productPrice * c.quantity) AS TotalPrice
                                    FROM Product p
                                    JOIN Cart c ON p.productId = c.productId
                                    WHERE c.customerId = @customerId";
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Connection = sqlconn;

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            totalPrice = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return totalPrice;
        }

        //------------------------------------------------------------------------------------------------------------
        public decimal GetTotalPriceOfOrdersByCustomer(int customerId)
        {
            decimal totalPrice = 0;

            try
            {
                using (SqlConnection sqlconn = new SqlConnection(connectionString))
                {
                    sqlconn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = @"SELECT SUM(oi.quantity * p.productPrice) AS TotalPrice
                                    FROM Orders o
                                    JOIN order_item oi ON o.orderId = oi.orderId
                                    JOIN Product p ON oi.productId = p.productId
                                    WHERE o.customerId = @customerId";
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Connection = sqlconn;

                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            totalPrice = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return totalPrice;
        }

        //------------------------------Update Product Stock Quantity----------------------------------------------
        public void UpdateStockQuantity(int productId, int quantity)
        {
            try
            {
                using (SqlConnection sqlconn = new SqlConnection(connectionString))
                {
                    sqlconn.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        
                        cmd.CommandText = "UPDATE Product SET StockQuantity =(StockQuantity- @quantity) WHERE ProductId = @ProductId";
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        cmd.Connection = sqlconn;

                        cmd.ExecuteNonQuery();

                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
        }

        


    }

}






     

        

        

        

      


