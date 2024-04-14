using EcommerceApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Repository
{
    public  interface IOrderProcessorRepository
    {
        bool CreateProduct(Product product);
        bool CreateCustomer(Customer customer);
        bool DeleteProduct(int productId);
        bool DeleteCustomer(int customerId);
        bool AddToCart(Customer customer, Product product, int quantity);
        bool RemoveFromCart(Customer customer, Product product);
        List<Cart> GetAllFromCart(Customer customer);
        bool PlaceOrder(Customer customer, List<Dictionary<Product, int>> productsAndQuantities, string shippingAddress);
        List<Dictionary<Product, int>> GetOrdersByCustomer(int customerId);


    }
}
