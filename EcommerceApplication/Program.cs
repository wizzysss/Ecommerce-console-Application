using EcommerceApplication.EcommerceApp;
using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using System;
using System.Xml.Linq;

namespace EcommerceApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            EcomApp ecomApp = new EcomApp();

            ecomApp.Run();

        }
    }
}
