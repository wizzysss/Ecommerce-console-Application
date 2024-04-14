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

        public CustomerNotFoundException() { }
        public CustomerNotFoundException(string message) : base(message)
        { 
        }

        

    }
}
