using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using EcommerceApplication.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        Product product = new Product();
        Customer customer = new Customer();
        OrderItem item = new OrderItem();
        Order order = new Order();

        public void Run()
        {
           
            OrderProcessorService orderProcessorService = new OrderProcessorService();
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(@"
 _____                                                                           _      ____    ____  
 | ____|   ___    ___    _ __ ___    _ __ ___     ___   _ __    ___    ___       / \    |  _ \  |  _ \ 
 |  _|    / __|  / _ \  | '_ ` _ \  | '_ ` _ \   / _ \ | '__|  / __|  / _ \     / _ \   | |_) | | |_) |
 | |___  | (__  | (_) | | | | | | | | | | | | | |  __/ | |    | (__  |  __/    / ___ \  |  __/  |  __/ 
 |_____|  \___|  \___/  |_| |_| |_| |_| |_| |_|  \___| |_|     \___|  \___|   /_/   \_\ |_|     |_|    
                                                                                                       
                                                                                                       
");
                Console.ResetColor();
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                Choose an Option:                 ║");
                Console.WriteLine("║                                                  ║");
                Console.WriteLine("║           1. Register Customer                   ║");
                Console.WriteLine("║           2. Login Customer                      ║");
                Console.WriteLine("║           3. Admin Controls                      ║");
                Console.WriteLine("║                                                  ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                int runinput = int.Parse(Console.ReadLine());
                switch (runinput)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("┌─────────────────────────────┐");
                        Console.WriteLine("│       REGISTER CUSTOMER     │");
                        Console.WriteLine("└─────────────────────────────┘");
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
                        
                        


                        Console.WriteLine("Loading ..........");
                        Thread.Sleep(4000);
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("┌─────────────────────────────┐");
                        Console.WriteLine("│       LOGIN CUSTOMER        │");
                        Console.WriteLine("└─────────────────────────────┘");
                        Console.WriteLine("Enter Customer Name: ");
                        string LoginName = Console.ReadLine();
                        Console.WriteLine("Enter password : ");
                        string LoginPassword = Console.ReadLine();
                        customer = new Customer()
                        {
                            Name = LoginName,            
                            Password = LoginPassword
                        };
                        if (orderprocess.CheckCustomerCredentials(LoginName, LoginPassword))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Login successful!");
                            Console.ResetColor();

                            orderprocess.DisplayCustomerId(LoginName, LoginPassword);
                            Thread.Sleep(2000);
                            Console.Clear();
                            CustomerOptions(LoginName,LoginPassword);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid login credentials.");
                            Console.ResetColor();
                            Thread.Sleep(2000);
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Enter Product admin password: ");
                        string adminPassword = "";
                        ConsoleKeyInfo key;
                        do
                        {
                            key = Console.ReadKey(true);                             
                            if (key.Key != ConsoleKey.Enter)
                            {
                                adminPassword += key.KeyChar;
                                Console.Write("*"); 
                            }
                        } while (key.Key != ConsoleKey.Enter);
                        if (adminPassword == "admin")
                        {
                            Console.Clear();
                            ProductControl();
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid Admin password");
                            Thread.Sleep(3000);
                        }
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

        public void CustomerOptions(string Name,string Password)
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                OrderProcessorService orderProcessorService = new OrderProcessorService();
                
                Console.WriteLine("┌──────────────────────────────────────────────────┐");
                Console.WriteLine("│                 CUSTOMER CONTROLS                │");
                Console.WriteLine("└──────────────────────────────────────────────────┘");
                orderprocess.DisplayCustomerId(Name, Password);
                int Loggedcustomerid = orderprocess.CustomerIdofLoggedIn(Name, Password); //can be used if wanted instead of customer id input
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                Menu Options                      ║");
                Console.WriteLine("╠══════════════════════════════════════════════════╣");
                Console.WriteLine("║            1. VIEW ALL PRODUCTS                  ║");
                Console.WriteLine("║            2. ADD PRODUCT TO YOUR CART           ║");
                Console.WriteLine("║            3. DELETE PRODUCT FROM CART           ║");
                Console.WriteLine("║            4. VIEW YOUR CART                     ║");
                Console.WriteLine("║            5. PLACE ORDER                        ║");
                Console.WriteLine("║            6. VIEW CUSTOMER ORDER                ║");
                Console.WriteLine("║            7. DELETE CUSTOMER ACCOUNT            ║");
                Console.WriteLine("║                                                  ║");
                Console.WriteLine("║               Press 0 to Logout                  ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");

                Console.WriteLine("Enter your Input");
                int userInput = int.Parse(Console.ReadLine());
                


                switch (userInput)
                {
                    case 1:
                        Console.Clear();
                        List<Product> productnamecustomer = orderprocess.ShowAllProductNames();
                        Console.WriteLine("Products available");
                        Console.WriteLine(".....................");
                        Console.WriteLine("Name      Product_id            Price");
                        foreach (Product product1 in productnamecustomer)
                        {
                            Console.WriteLine(product1.Name + "\t\t" + product1.ProductId +"\t\t"+product1.Price);
                        }
                        break;

                    case 2:
                        Console.Clear();
                        List<Product> productnamecart= orderprocess.ShowAllProductNames();
                        Console.WriteLine("          Products available           ");
                        Console.WriteLine(".......................................");
                        Console.WriteLine("Name      Product_id            Price");
                        Console.WriteLine(".......................................");
                        foreach (Product product1 in productnamecart)
                        {
                            Console.WriteLine(product1.Name + "\t\t" + product1.ProductId + "\t\t" + product1.Price);
                        }
                        int customerid = Loggedcustomerid;
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

                                    int morecustomerid = Loggedcustomerid;
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

                    case 3:

                        int deletecustomerid = Loggedcustomerid;
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

                    case 4:
                        Console.Clear();

                        int cartcustomerid = Loggedcustomerid;
                        customer = new Customer()
                        {
                            CustomerId = cartcustomerid
                        };

                        orderProcessorService.DisplayCartRecordservice(customer);
                        break;
                    case 5:
                        Console.Clear();
                        // Get input for placing order

                        int customerIdo = Loggedcustomerid;
                        customer = new Customer() { CustomerId = customerIdo };

                        Console.WriteLine("Enter shipping address:");
                        string shippingAddress = Console.ReadLine();

                        // Fetch cart items for the customer
                        List<Cart> cartItems = orderprocess.GetAllFromCart(customer);

                        // Prepare productsAndQuantities list using cart items
                        List<Dictionary<Product, int>> productsAndQuantities = new List<Dictionary<Product, int>>();
                        foreach (var cartItem in cartItems)
                        {
                            // Prompt user to confirm adding each product to the order
                            Console.WriteLine($"Add product '{cartItem.ProductId}' to order? (yes/no)");
                            string response = Console.ReadLine().ToLower();

                            if (response == "yes")
                            {
                                // Create dictionary entry for the product and its quantity from the cart
                                Dictionary<Product, int> productQuantityPair = new Dictionary<Product, int>();
                                Product product1 = new Product() { ProductId = cartItem.ProductId.Value }; // Assuming Product class has appropriate properties
                                productQuantityPair.Add(product1, cartItem.Quantity.Value);
                                productsAndQuantities.Add(productQuantityPair);
                            }
                        }

                        // Call the PlaceOrder method with the gathered input
                        bool orderPlaced = orderProcessorService.PlaceOrderservice(customer, productsAndQuantities, shippingAddress);
                        if (orderPlaced)
                        {
                            Console.WriteLine("Order placed successfully!");

                        }
                        else
                        {
                            Console.WriteLine("Failed to place order.");
                        }
                        break;
                        
                    case 6:
                        Console.Clear();

                        int customerId = Loggedcustomerid;
                        List<Dictionary<Product, int>> orders = orderprocess.GetOrdersByCustomer(customerId);
                        if (orders.Count > 0)
                        {
                            Console.WriteLine($"Orders for customer {customerId}:");
                            foreach (var orderi in orders)
                            {
                                Console.WriteLine("Order:");
                                foreach (var kvp in orderi)
                                {
                                    Console.WriteLine($"Product: {kvp.Key.Name}, Quantity: {kvp.Value}");
                                }
                                Console.WriteLine();
                            }
                            decimal Totalprice=orderprocess.GetTotalPriceByCustomerId(Loggedcustomerid);
                            Console.WriteLine("The total Price of your orders:$" + Totalprice);
                            Thread.Sleep(5000);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine($"No orders found for customer {customerId}");
                        }
                        break;

                        case 7:
                       
                        int deletecid = Loggedcustomerid;
                        Console.WriteLine("Do you really want to Delete your account");
                        Console.WriteLine("then type YES");
                        string deletecustomer = (Console.ReadLine());
                        if (deletecustomer == "YES")
                        orderProcessorService.Deletecustomerservice(deletecid);

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

        public void ProductControl()
        {
            OrderProcessorService orderProcessorService = new OrderProcessorService();
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.WriteLine("┌────────────────────────────────────────────────-─┐");
                Console.WriteLine("│              ADMIN & PRODUCT CONTROL             │");
                Console.WriteLine("└──────────────────────────────────────────────────┘");
                Console.WriteLine("  ");
                Console.WriteLine("╔══════════════════════════════════════════════════╗");
                Console.WriteLine("║                Choose an Option:                 ║");
                Console.WriteLine("║                                                  ║");
                Console.WriteLine("║          1. Add a new product                    ║");
                Console.WriteLine("║          2. View all Products with id            ║");
                Console.WriteLine("║          3. Delete an Existing Product           ║");
                Console.WriteLine("║          4. View product Stock available         ║");
                Console.WriteLine("║          5. Delete customer Account              ║");
                Console.WriteLine("║                                                  ║");
                Console.WriteLine("║                 Press 0 to Exit                  ║");
                Console.WriteLine("╚══════════════════════════════════════════════════╝");
                int runinput = int.Parse(Console.ReadLine());
                switch (runinput)
                {
                    case 1:
                        Console.Clear();
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
                        Console.Clear();
                        List<Product> productname = orderprocess.ShowAllProductNames();
                        Console.WriteLine("Products available");
                        Console.WriteLine(".....................");
                        Console.WriteLine("Name      Product_id            Price");
                        foreach (Product product1 in productname)
                        {
                            Console.WriteLine(product1.Name + "\t\t" + product1.ProductId + "\t\t" + product1.Price);
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Enter Product Id: ");
                        int deletepid = int.Parse(Console.ReadLine());
                        orderProcessorService.Deleteproductservice(deletepid);
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("Enter Product Id: ");
                        int stockproductid = int.Parse(Console.ReadLine());
                        orderprocess.GetProductStockQuantity(stockproductid);
                        break;
                    case 5:
                        Console.WriteLine("Enter Customer Id: ");
                        int deletecid = int.Parse(Console.ReadLine());
                        orderProcessorService.Deletecustomerservice(deletecid);
                        break;
                    case 0:
                        continueRunning = false;

                        break;


                }
               
              
            }

        }
    }
}
