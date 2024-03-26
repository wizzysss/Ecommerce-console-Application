using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message) { 
        }

        public static void productnotfound(int productid)
        {
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
            if (!orderProcessorRepositoryImpl.ProductNotExist(productid))
                throw new ProductNotFoundException("Product not found!!!");

        }
        public static void productnotincart(int productid,int customerid)
        {

            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
            if (!orderProcessorRepositoryImpl.ProductNotExistinCart(productid,customerid))
                throw new ProductNotFoundException("Product not found in cart for the customer!!!");
        }

        public static void NotEnoughStock(int stockvalue,int productid)
        {
            Product product = new Product();    
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
            if (stockvalue > orderProcessorRepositoryImpl.availablestockquantity(productid))
            {
                throw new ProductNotFoundException("Not Enough stock");
            }
        }
    }
}
