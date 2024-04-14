using EcommerceApplication.Exceptions;
using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EcommerceApplication.Service
{
    public class OrderProcessorService
    {
        private readonly OrderProcessorRepositoryImpl OPRI;
        public OrderProcessorService() {

            OPRI = new OrderProcessorRepositoryImpl();
        }

        public void Createproductservice(Product product)
        {
            try
            {

                if (product.Price < 0)
                {
                    throw new InvalidProductException("Invalid Price amount");
                }
                OPRI.CreateProduct(product);
                Console.WriteLine("Record inserted successfully");
            }
            catch (InvalidProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Createcustomerservice(Customer customer)
        {

            try
            {
                if (!customer.Email.Contains('@'))
                {
                    throw new InvalidCustomerException("Invalid Email");
                }
                OPRI.CreateCustomer(customer);
                Console.WriteLine("Customer inserted successfully");
                OPRI.GetRecentcustID();
            }
            catch (InvalidCustomerException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Deleteproductservice(int product)
        {
            try
            {

                if (!OPRI.ProductNotExist(product))
                    throw new ProductNotFoundException("Product not found!!!");

                OPRI.DeleteProduct(product);
                Console.WriteLine("Product deleted");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Deletecustomerservice(int customerid)
        {
            try
            {
                if (!OPRI.CustomerNotExist(customerid))
                    throw new CustomerNotFoundException("Customer not found!!!");
                OPRI.DeleteCustomer(customerid);
                Console.WriteLine("Customer deleted");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Addtocartservice(Customer customer, Product product, int quantity)
        {


            try
            {

                if (!OPRI.ProductNotExist(product.ProductId))
                    throw new ProductNotFoundException("Product not found!!!");
                if (quantity > OPRI.Availablestockquantity(product.ProductId))
                {
                    throw new ProductNotFoundException("Not Enough stock");
                }

                OPRI.AddToCart(customer, product, quantity);
                Console.WriteLine("Product added to cart ");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
        public void Deletefromcartservice(Customer customer, Product product)
        {
            try
            {
                if (!OPRI.CustomerNotExist(customer.CustomerId))
                    throw new CustomerNotFoundException("Customer not found!!!");
                if (!OPRI.ProductNotExistinCart(product.ProductId, customer.CustomerId))
                    throw new ProductNotFoundException("Product not found in cart for the customer!!!");
                OPRI.RemoveFromCart(customer, product);
                Console.WriteLine("Order deleted from cart ");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ProductNotFoundException ex) {
                Console.WriteLine(ex.Message);
            }

        }

        public void DisplayCartRecordservice(Customer customerid)
        {
            try {
                if (!OPRI.CustomerNotExist(customerid.CustomerId))
                    throw new CustomerNotFoundException("Customer not found!!!");
                List<Cart> cartList = OPRI.GetAllFromCart(customerid);
                Console.WriteLine("cartid\tcustomerid\tproductid\tquantity");
                foreach (Cart cart1 in cartList)
                {
                    Console.WriteLine(cart1.CartId + "\t" + cart1.CustomerId + "\t\t" + cart1.ProductId + "\t\t" + cart1.Quantity);
                }

            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool PlaceOrderservice(Customer customer, List<Dictionary<Product, int>> productsAndQuantities, string shippingAddress)
        {
            try
            {
                if (!OPRI.CustomerNotExist(customer.CustomerId))
                    throw new CustomerNotFoundException("Customer not found!!!");
                OPRI.PlaceOrder(customer, productsAndQuantities, shippingAddress);
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
            return true;

        }
        public void GetOrdersByCustomerservice(int customerId)
        {
            try
            {
                if (!OPRI.OrderNotExist(customerId))
                    throw new OrderNotFoundException("Order not found fro the customer");
                OPRI.GetOrdersByCustomer(customerId);
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
        }

        



    }

}
