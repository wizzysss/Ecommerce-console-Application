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
        public InvalidProductException() { 
        }
        public InvalidProductException(string message) : base(message)
        {

        }
        
    }
}
