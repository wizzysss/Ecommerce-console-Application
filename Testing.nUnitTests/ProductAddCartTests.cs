using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using EcommerceApplication.Service;

namespace Testing.nUnitTests
{
    public class ProductAddCartTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void ProductAddToCartTest()
        {
            OrderProcessorService orderProcessorService = new OrderProcessorService();
          

            Customer customer = new Customer()
            {
                CustomerId = 2
            };
            Product product = new Product()
            {

                ProductId = 1,
            };
            int quantity = 2;

            bool addProducttocart = orderProcessorService.Addtocartservice(customer, product, quantity);
            Assert.That(true, Is.EqualTo(addProducttocart));
        }

    }
}