using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using EcommerceApplication.Service;

namespace Testing.nUnitTests
{
    public class ProductOrderedTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void ProductInOrder() { 
        
            OrderProcessorService orderProcessorService = new OrderProcessorService();

                Customer customer = new Customer()
                {
                    CustomerId = 1,
                };

                Product product = new Product()
                {
                    ProductId = 2,
                };

                OrderItem orderItem = new OrderItem()
                {
                    Quantity = 3,
                };
                Order order = new Order()
                {
                    ShippingAddress = "avadi",
                };



                bool ProductedAdded = orderProcessorService.Placeorderservice(customer, order, orderItem, product);


            Assert.That(true, Is.EqualTo(ProductedAdded));
        }

    }
}