using EcommerceApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Exceptions
{
    internal class InvalidProductException : Exception
    {
        public InvalidProductException(string message) : base(message)
        {

        }
        public static void InvalidProductData(Product product)
        {
            if (product.Price < 0)
            {
                throw new InvalidProductException("Invalid Price amount");
            }
        }
    }
}
