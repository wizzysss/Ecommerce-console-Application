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

        public InvalidCustomerException() { }

        public InvalidCustomerException(string message) : base(message)
        { 

        }

       


    }
}
