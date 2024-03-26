using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Exceptions
{
    internal class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message)
        { 
        }

        public static void customernotfound(int customerid)
        {
            OrderProcessorRepositoryImpl orderProcessorRepositoryImpl = new OrderProcessorRepositoryImpl();
            if(!orderProcessorRepositoryImpl.CustomerNotExist(customerid))
             throw new CustomerNotFoundException("Customer not found!!!");

        }

    }
}
