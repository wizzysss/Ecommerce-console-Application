using EcommerceApplication.Exceptions;
using EcommerceApplication.Models;
using EcommerceApplication.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EcommerceApplication.Repository
{
    public class OrderProcessorRepositoryImpl : IOrderProcessorRepository
    {

        SqlConnection connect = null;
        SqlCommand cmd = null;
        public OrderProcessorRepositoryImpl()
        {
            connect = new SqlConnection(DataConnectionUtility.GetConnectionString());
            cmd = new SqlCommand();
        }

        Customer customer = new Customer();
        Order order = new Order();
        Product product = new Product();

        public bool CreateProduct(Product product)
        {
            cmd.CommandText = "Insert into products values(@name,@price,@desc,@sq)";
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@desc", product.Description);
            cmd.Parameters.AddWithValue("@sq", product.StockQuantity);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connect.Close();
            return true;
        }

        public bool CreateCustomer(Customer customer)
        {
            cmd.CommandText = "Insert into customers values(@name,@email,@password)";
            cmd.Parameters.AddWithValue("@name", customer.Name);
            cmd.Parameters.AddWithValue("@email", customer.Email);
            cmd.Parameters.AddWithValue("@password", customer.Password);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            connect.Close();
            return true;
        }

        public bool DeleteProduct(int productId)
        {
            cmd.CommandText = "delete from products where product_id = @pid";
            cmd.Parameters.AddWithValue("@pid", productId);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
            cmd.Parameters.Clear();
            return true;
        }

        public bool DeleteCustomer(int customerId)
        {
            cmd.CommandText = "delete from customers where customer_id = @cid";
            cmd.Parameters.AddWithValue("@cid", customerId);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
            cmd.Parameters.Clear();
            return true;
        }

        public bool AddToCart(Customer customer, Product product, int quantity)
        {
        

                cmd.CommandText = "Insert into cart values(@cusid,@proid,@quantity)";
                cmd.Parameters.AddWithValue("@cusid", customer.CustomerId);
                cmd.Parameters.AddWithValue("@proid", product.ProductId);
                cmd.Parameters.AddWithValue("@quantity", quantity);

                connect.Open();
                cmd.Connection = connect;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                connect.Close();
                cmd.Parameters.Clear();
                return true;

        }

        public bool RemoveFromCart(Customer customer, Product product)
        {
            cmd.CommandText = "delete from cart where customer_id = @removecid and product_id=@rpid";
            cmd.Parameters.AddWithValue("@removecid", customer.CustomerId);
            cmd.Parameters.AddWithValue("@rpid", product.ProductId);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            connect.Close();
            cmd.Parameters.Clear();
            return true;
        }

        public List<Cart> GetAllFromCart(Customer customer)
        {
            List<Cart> cartList = new List<Cart>();

            cmd.CommandText = "SELECT * FROM cart WHERE customer_id = @customerid";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@customerid", customer.CustomerId);

            connect.Open();
            cmd.Connection = connect;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Cart cart1 = new Cart();
                cart1.CartId = (int)reader["cart_id"];
                cart1.CustomerId = Convert.IsDBNull(reader["customer_id"]) ? null : (int)reader["customer_id"];
                cart1.ProductId = Convert.IsDBNull(reader["product_id"]) ? null : (int)reader["product_id"];
                cart1.Quantity = Convert.IsDBNull(reader["quantity"]) ? null : (int)reader["quantity"];
                cartList.Add(cart1);
            }

            connect.Close();
            

            return cartList;
        }

     

        public bool PlaceOrder(Customer customer, List<Dictionary<Product, int>> productsAndQuantities, string shippingAddress)
        {
            try
            {
                List<Cart> cartItems = GetAllFromCart(customer);
                int orderId = CreateOrder(customer, shippingAddress, cartItems);
                foreach (var productQuantityPair in productsAndQuantities)
                {
                    foreach (var kvp in productQuantityPair)
                    {
                        Product product = kvp.Key;
                        int quantity = kvp.Value;
                        InsertOrderItem(orderId, product, quantity);
                        UpdateProductStock(product.ProductId, quantity);
                    }
                }
                RemoveItemsFromCart(customer);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

      
        private int CreateOrder(Customer customer, string shippingAddress, List<Cart> cartItems)
        {
            int generatedOrderId = 0;
                decimal totalPrice = CalculateTotalPrice(cartItems);
                cmd.CommandText = "INSERT INTO orders (customer_id, order_date, total_price, shipping_address) OUTPUT INSERTED.order_id VALUES (@customerId_order, @orderDate, @totalPrice, @shippingAddress); ";
                cmd.Parameters.AddWithValue("@customerId_order", customer.CustomerId);
                cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@shippingAddress", shippingAddress);
                cmd.Parameters.AddWithValue("@totalPrice", totalPrice);
                connect.Open();
                cmd.Connection = connect;
                object result = cmd.ExecuteScalar();
                generatedOrderId = Convert.ToInt32(result);
                Console.WriteLine("Order Total price is $"+totalPrice);
                cmd.Parameters.Clear();
                connect.Close();        
            return generatedOrderId;
        }

        public decimal CalculateTotalPrice(List<Cart> cartItems)
        {
            decimal totalPrice = 0;
            foreach (var cartItem in cartItems)
            {
                decimal productPrice = GetProductPrice(cartItem.ProductId ?? 0); 
                totalPrice += productPrice * (cartItem.Quantity ?? 0);
            }
            return totalPrice;
        }


        private void InsertOrderItem(int orderId, Product product, int quantity)
        {
            cmd.CommandText = "INSERT INTO order_items (order_id, product_id, quantity) VALUES (@orderId_item, @productId, @quantity);";
            cmd.Parameters.AddWithValue("@orderId_item", orderId);
            cmd.Parameters.AddWithValue("@productId", product.ProductId);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            connect.Close();
        }

        private void RemoveItemsFromCart(Customer customer)
        {
            cmd.CommandText = "DELETE FROM cart WHERE customer_id = @customerId_remove_cart";
            cmd.Parameters.AddWithValue("@customerId_remove_cart", customer.CustomerId);

            connect.Open();
            cmd.Connection = connect;
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear(); 
            connect.Close();
        }
        public decimal GetProductPrice(int productId)
        {
            decimal productprice = 0m;
                connect.Open();
                cmd.Connection = connect;
                cmd.CommandText = "SELECT price FROM products WHERE product_id = @productid";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@productid", productId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        productprice = reader.GetInt32(reader.GetOrdinal("price"));
                    }
                }         
                connect.Close();
            return productprice;
        }

        private void UpdateProductStock(int productId, int quantity)
        {
            try
            {
                cmd.CommandText = "UPDATE products SET stockQuantity = stockQuantity - @quantity WHERE product_id = @productId AND stockQuantity >= @quantity;";
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@productId", productId);

                connect.Open();
                cmd.Connection = connect;
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("Not enough stock for product ID " + productId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating product stock: " + ex.Message);
                throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                connect.Close();
            }
        }



        public List<Dictionary<Product, int>> GetOrdersByCustomer(int customerId)
        {
            List<Dictionary<Product, int>> orders = new List<Dictionary<Product, int>>();
                using (SqlConnection connect = new SqlConnection(DataConnectionUtility.GetConnectionString()))
                using (SqlCommand cmd = connect.CreateCommand())
                {
                    if (connect.State != ConnectionState.Open)
                        connect.Open();

                    // Construct the SQL command to fetch orders by customer
                    cmd.CommandText = "SELECT o.order_id, oi.product_id, oi.quantity " +
                                      "FROM orders o " +
                                      "JOIN order_items oi ON o.order_id = oi.order_id " +
                                      "WHERE o.customer_id = @customerId";

                    // Clear previous parameters before adding new ones
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<int, Dictionary<Product, int>> tempOrders = new Dictionary<int, Dictionary<Product, int>>();
                        while (reader.Read())
                        {
                            int orderId = Convert.ToInt32(reader["order_id"]);
                            int productId = Convert.ToInt32(reader["product_id"]);
                            int quantity = Convert.ToInt32(reader["quantity"]);
                            if (!tempOrders.ContainsKey(orderId))
                            {
                                tempOrders[orderId] = new Dictionary<Product, int>();
                            }
                            Product product = GetProductDetails(productId); 
                            if (product != null)
                            {
                                tempOrders[orderId].Add(product, quantity);
                            }
                        }
                        orders = tempOrders.Values.ToList();
                    }
                }
            return orders;
        }

        public Product GetProductDetails(int productId)
        {
            Product product = null;

            try
            {
                using (SqlConnection connect = new SqlConnection(DataConnectionUtility.GetConnectionString()))
                using (SqlCommand cmd = connect.CreateCommand())
                {
                 
                    if (connect.State != ConnectionState.Open)
                        connect.Open();
                    cmd.CommandText = "SELECT * FROM products WHERE product_id = @productId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@productId", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                ProductId = (int)reader["product_id"],
                                Name = reader["name"].ToString(),
                                Price = Convert.ToInt32(reader["price"]),
                                Description = reader["description"].ToString(),
                                StockQuantity = (int)reader["stockQuantity"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching product details: " + ex.Message);
            }

            // Return the fetched product (or null if not found)
            return product;
        }



        internal bool CustomerNotExist(int customerid)
        {
            int count = 0;
            cmd.CommandText = "Select count(*) as total from customers where customer_id=@cid1";
            cmd.Parameters.AddWithValue("@cid1", customerid);
            connect.Open();
            cmd.Connection = connect;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmd.Parameters.Clear();
                count = (int)reader["total"];
            }
            connect.Close();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        internal bool OrderNotExist(int customerid)
        {
            int count = 0;
            cmd.CommandText = "Select count(*) as total from orders where customer_id=@custoid";
            cmd.Parameters.AddWithValue("@custoid", customerid);
            connect.Open();
            cmd.Connection = connect;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmd.Parameters.Clear();
                count = (int)reader["total"];
            }
            connect.Close();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool ProductNotExist(int productid)
        {
            int count = 0;
            cmd.CommandText = "Select count(*) as total from products where product_id=@pid3";
            cmd.Parameters.AddWithValue("@pid3", productid);
            connect.Open();
            cmd.Connection = connect;
            
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmd.Parameters.Clear();
                count = (int)reader["total"];
            }
            connect.Close();
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        internal bool ProductNotExistinCart(int productid,int customerid)
        {
            int count = 0;
            cmd.CommandText = "Select count(*) as total from cart where product_id=@pid4 and customer_id=@cid4";
            cmd.Parameters.AddWithValue("@pid4", productid);
            cmd.Parameters.AddWithValue("@cid4", customerid);
            connect.Open();
            cmd.Connection = connect;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmd.Parameters.Clear();
                count = (int)reader["total"];
            }
            connect.Close();
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        internal int Availablestockquantity(int productid)
        {
            int count = 0;
            cmd.CommandText = "Select stockQuantity as avalquantity from products where product_id=@pid5";

            cmd.Parameters.AddWithValue("@pid5", productid);
            connect.Open();
            cmd.Connection = connect;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmd.Parameters.Clear();
                count = (int)reader["avalquantity"];
            }
            connect.Close();
            return count;        
        }

        public List<Product> ShowAllProductNames()
        {
            List<Product> ProductName= new List<Product>();

            cmd.CommandText = "SELECT name,product_id,price from products ";
            cmd.Parameters.Clear();

            connect.Open();
            cmd.Connection = connect;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Product product = new Product();
                product.Name = (string)reader["Name"];
                product.ProductId = (int)reader["product_id"];
                product.Price = Convert.ToInt32(reader["price"]);
                ProductName.Add(product);
                
            }

            connect.Close();

            return ProductName;
            

        }

        public bool CheckCustomerCredentials(string customerName, string password)
        {
            bool isValid = false;

           

            cmd.CommandText = "SELECT COUNT(*) FROM customers WHERE Cust_name = @customerName AND password = @password";
            cmd.Parameters.AddWithValue("@customerName", customerName);
            cmd.Parameters.AddWithValue("@password", password);

            connect.Open();
            cmd.Connection = connect;
            int Count = Convert.ToInt32(cmd.ExecuteScalar());

            // If count is greater than 0, it means the customer with the given name and password exists
            isValid = Count > 0;


            cmd.Parameters.Clear();
            connect.Close();


            return isValid;
        }

        public void GetRecentcustID()
        {
            int recentid=-1;
            cmd.CommandText=("SELECT TOP 1 customer_id FROM customers ORDER BY customer_id DESC");
            connect.Open();
            cmd.Connection = connect;
           

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                recentid = reader.GetInt32(0);
            }
            reader.Close();
            connect.Close();
            Console.WriteLine("...........................");
            Console.WriteLine("|YOUR CUSTOMER ID is "+recentid+"|");
            Console.WriteLine("...........................");
        }
        public void DisplayCustomerId(string customerName, string password)
        {
                connect.Open();
                cmd.CommandText = "SELECT customer_id FROM customers WHERE Cust_name = @customerName AND password = @password";
                cmd.Parameters.AddWithValue("@customerName", customerName);
                cmd.Parameters.AddWithValue("@password", password);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    int customerId = Convert.ToInt32(result);
                    Console.WriteLine($"Logged in customer ID: {customerId}");
                }
                else
                {
                    Console.WriteLine("Customer not found or incorrect credentials.");
                }
                      
                cmd.Parameters.Clear();
                connect.Close();
            
        }
        public void GetProductStockQuantity(int productId)
        {
            int stockQuantity = 0;
                connect.Open();
                cmd.CommandText = "SELECT stockQuantity FROM products WHERE product_id = @productId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Connection = connect;
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    stockQuantity = Convert.ToInt32(result);
                }
            else
            {
                Console.WriteLine("NO product quantity is null");
            }
                connect.Close();
            Console.WriteLine("Stock Available is "+stockQuantity);
        }

        public int CustomerIdofLoggedIn(string customerName, string password)
        {
            int customerID = 0;
            cmd.CommandText = "Select customer_id as ID from customers where Cust_name=@name and password=@password";

            cmd.Parameters.AddWithValue("@name", customerName);
            cmd.Parameters.AddWithValue("@password", password);
            connect.Open();
            cmd.Connection = connect;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmd.Parameters.Clear();
                customerID = (int)reader["ID"];
            }
            connect.Close();
            return customerID;
        }
        public decimal GetTotalPriceByCustomerId(int customerId)
        {
            decimal totalPrice = 0;
            cmd.CommandText = "SELECT SUM(total_price) as Total FROM orders WHERE customer_id = @customerId";

            cmd.Parameters.AddWithValue("@customerId", customerId);
            connect.Open();
            cmd.Connection = connect;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmd.Parameters.Clear();
                totalPrice = (decimal)reader["Total"];
            }
            connect.Close();
            return totalPrice;
        }
    }
}
