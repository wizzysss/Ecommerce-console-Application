using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using EcommerceApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.EcommerceApp
{
    internal class EcomApp
    {
        readonly OrderProcessorRepositoryImpl orderprocess;

        public EcomApp()
        {
           orderprocess = new OrderProcessorRepositoryImpl();
        }

        public void Run()
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                OrderProcessorService orderProcessorService = new OrderProcessorService();
                Console.WriteLine("Ecommerce");
                Console.WriteLine("1.Add product in product table");
                Console.WriteLine("2.Add Customer in customer table");
                Console.WriteLine("3.Delete Product in product table");
                Console.WriteLine("4.Delete Customer in customer table");
                Console.WriteLine("5.Add product to cart");
                Console.WriteLine("6.Delete product from cart");
                Console.WriteLine("7.Display cart for a customer");
                Console.WriteLine("8.Place order");
                Console.WriteLine("9.Get Order by customer:");
                Console.WriteLine("Enter your Input");
                int userInput = int.Parse(Console.ReadLine());
                Product product = new Product();
                Customer customer = new Customer();
               OrderItem item = new OrderItem();
                Order order = new Order();


                switch (userInput)
                {
                    case 1:
                        Console.WriteLine("Enter Product Name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Product price: ");
                        int price = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Product description : ");
                        string desc = Console.ReadLine();
                        Console.WriteLine("Enter stock qunatity: ");
                        int sq = int.Parse(Console.ReadLine());
                        product = new Product() { Name = name, Price = price, Description = desc, StockQuantity = sq };
                        orderProcessorService.Createproductservice(product);
                        break;

                    case 2:
                        Console.WriteLine("Enter Name: ");
                        string cname = Console.ReadLine();
                        Console.WriteLine("Enter Email: ");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter password : ");
                        string password = Console.ReadLine();
                        customer = new Customer()
                        {
                            Name = cname,
                            Email = email,
                            Password = password
                        };
                        orderProcessorService.Createcustomerservice(customer);
                        break;

                    case 3:
                        Console.WriteLine("Enter Product Id: ");
                        int deletepid = int.Parse(Console.ReadLine());
                        orderProcessorService.Deleteproductservice(deletepid);
                        break;

                    case 4:
                        Console.WriteLine("Enter Customer Id: ");
                        int deletecid = int.Parse(Console.ReadLine());
                        orderProcessorService.Deletecustomerservice(deletecid);
                        break;
                    case 5:

                        Console.WriteLine("enter the customer id:");
                        int customerid = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter the product id:");
                        int productid = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter the quantity:");
                        int quantity = int.Parse(Console.ReadLine());
                        customer = new Customer()
                        {
                            CustomerId = customerid,
                        };
                        product = new Product()
                        {
                            ProductId = productid,
                        };
                        orderProcessorService.Addtocartservice(customer, product, quantity);

                        Console.WriteLine("Want to add more product to cart then press one");

                        int inner = int.Parse(Console.ReadLine());
                        while (inner == 1)
                        {

                            switch (inner)
                            {

                                case 1:
                                    Console.WriteLine("enter the customer id:");
                                    int morecustomerid = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the product id:");
                                    int moreproductid = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the quantity:");
                                    int morequantity = int.Parse(Console.ReadLine());
                                    customer = new Customer()
                                    {
                                        CustomerId = morecustomerid,
                                    };
                                    product = new Product()
                                    {
                                        ProductId = moreproductid,
                                    };
                                    orderProcessorService.Addtocartservice(customer, product, morequantity);
                                    Console.WriteLine("do you want more then press 1 or to exit press 0");
                                    int over = int.Parse(Console.ReadLine());
                                    inner = over;
                                    break;
                            }
                        }
                        break;
                    case 6:
                        Console.WriteLine("enter the customer id:");
                        int deletecustomerid = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter the product id:");
                        int deleteeproductid = int.Parse(Console.ReadLine());
                        customer = new Customer()
                        {
                            CustomerId = deletecustomerid
                        };
                        product = new Product()
                        {
                            ProductId = deleteeproductid
                        };
                        orderProcessorService.Deletefromcartservice(customer, product);
                        break;

                        case 7: 

                        Console.WriteLine("Enter Customer id:");
                        int cartcustomerid = int.Parse(Console.ReadLine());
                        customer = new Customer()
                        {
                            CustomerId = cartcustomerid
                        };

                        orderProcessorService.DisplayCartRecordservice(customer);
                        break;

                        case 8:                      


                        Console.WriteLine("Enter Customer id:");
                        int customeridorder = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Product id:");
                        int productidorder = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter quantity:");
                        int quantityorder = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Address:");
                        string address = Console.ReadLine();

                        
                        customer = new Customer()
                        {
                            CustomerId = customeridorder
                        };
                        product = new Product()
                        {
                            ProductId = productidorder
                        };
                        item = new OrderItem()
                        {
                            Quantity = quantityorder
                        };

                        order = new Order()
                        {
                            ShippingAddress = address
                        };                  
                        orderProcessorService.Placeorderservice(customer,order,item,product);
                        break;

                    case 9:

                        Console.WriteLine("Enter Customer id:");
                        int cust = int.Parse(Console.ReadLine());
                        order = new Order()
                        {
                            CustomerId = cust
                        };
                        orderProcessorService.DisplayOrderbyCustomer(order);

                        break;


                    case 0:
                        continueRunning = false; 

                        break;

                    default:
                        Console.WriteLine("Try again");
                        break;

                }
               
            }
        }

    }
}
