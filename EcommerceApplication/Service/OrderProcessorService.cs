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
    public  class OrderProcessorService
    {
        private readonly OrderProcessorRepositoryImpl OPRI;
        public OrderProcessorService() {

            OPRI = new OrderProcessorRepositoryImpl();
        }

        public void Createproductservice(Product product)
        {
            try
            {
                InvalidProductException.InvalidProductData(product);
                OPRI.CreateProduct(product);
                Console.WriteLine("Record inserted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Createcustomerservice(Customer customer)
        {

            try
            {
                InvalidCustomerException.InvalidCustomerData(customer);
                OPRI.CreateCustomer(customer);
                Console.WriteLine("Customer inserted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Deleteproductservice(int product)
        {
            try
            {
                ProductNotFoundException.productnotfound(product);
                OPRI.DeleteProduct(product);
                Console.WriteLine("Product deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Deletecustomerservice(int customerid)
        {
            try
            {
                CustomerNotFoundException.customernotfound(customerid);
                OPRI.DeleteCustomer(customerid);
                Console.WriteLine("Customer deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool Addtocartservice(Customer customer, Product product, int quantity)
        {


            try
            {
                
                ProductNotFoundException.productnotfound(product.ProductId);
                ProductNotFoundException.NotEnoughStock(quantity,product.ProductId);
                OPRI.AddToCart(customer, product, quantity);
                Console.WriteLine("Product added to cart ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
        public void Deletefromcartservice(Customer customer, Product product)
        {
            try
            {
                CustomerNotFoundException.customernotfound(customer.CustomerId);
                ProductNotFoundException.productnotincart(product.ProductId,customer.CustomerId);
                OPRI.RemoveFromCart(customer, product);
                Console.WriteLine("Order deleted from cart ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
       
        public void DisplayCartRecordservice(Customer customerid)
        {
            try { 
            CustomerNotFoundException.customernotfound(customerid.CustomerId);
                List<Cart> cartList = OPRI.GetAllFromCart(customerid);
                Console.WriteLine("cartid\tcustomerid\tproductid\tquantity");
                foreach (Cart cart1 in cartList)
                {
                    Console.WriteLine(cart1.CartId + "\t" + cart1.CustomerId + "\t\t" + cart1.ProductId + "\t\t" + cart1.Quantity);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public bool Placeorderservice(Customer customer, Order order, OrderItem orderitem, Product product)
        {
            try
            {
                CustomerNotFoundException.customernotfound(customer.CustomerId);
                ProductNotFoundException.productnotfound(product.ProductId);
                ProductNotFoundException.NotEnoughStock(orderitem.Quantity, product.ProductId);
                OPRI.PlaceOrder(customer, order, orderitem, product);
                Console.WriteLine("Order placed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }

        public void DisplayOrderbyCustomer(Order order)
        {

            try
            {
                //OrderNotFoundException.ordernotfound(order.OrderId);
                List<OrderItem> orderList = OPRI.GetOrderByCustomer(order);
                Console.WriteLine("Productid\tQuantity");
                foreach (OrderItem list in orderList)
                {
                    Console.WriteLine(list.ProductId + "\t\t" + list.Quantity);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}
