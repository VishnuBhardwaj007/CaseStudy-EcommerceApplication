using Ecommerce_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Repository
{
    internal interface IOrderProcessorRepository
    {
        bool CreateProduct(Product product);
        bool CreateCustomer(Customer customer);

        bool DeleteProduct(int  ProductId);
        bool DeleteCustomer(int customerId);
        bool AddToCart(Customer customer,Product product,int quantity);
        bool RemoveFromCart(Customer customer,Product product);

        List<string> GetAllFromCart(Customer customer);
        bool PlaceOrder(Customer customer, List<KeyValuePair<Product, int>> productsAndQuantities, string shippingAddress);

        public List<KeyValuePair<string, int>> GetOrdersByCustomer(int customerId);

    }
}
