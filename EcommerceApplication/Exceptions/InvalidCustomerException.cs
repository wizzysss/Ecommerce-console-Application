using EcommerceApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Exceptions 
{
    internal class InvalidCustomerException : Exception
    {

        public InvalidCustomerException(string message) : base(message)
        { 

        }

        public static void InvalidCustomerData(Customer customer)
        {
            if (!customer.Email.Contains('@'))
            {
                throw new InvalidCustomerException("Invalid Email");
            }
        }


    }
}
