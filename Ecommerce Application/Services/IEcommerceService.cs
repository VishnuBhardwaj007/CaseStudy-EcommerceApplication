using Ecommerce_Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Application.Services
{
    internal interface IEcommerceService
    {
        void Createproduct();

        void Login();
        void RegisterCustomer();


        void DeleteProduct();
        void DeleteCustomer();
        void AddToCart();
        void RemoveFromCart();

        void GetAllFromCart();

        void GetOrdersByCustomer();

        void PlaceOrder();

    }
}
