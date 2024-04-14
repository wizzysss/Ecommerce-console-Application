using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Exceptions
{
    internal class OrderNotFoundException :Exception
    {
        public OrderNotFoundException() { 
        }
        public OrderNotFoundException(string message) : base(message)
        {
        }

        public static void ordernotfound(int orderid)
        {
           

        }
    }
}
