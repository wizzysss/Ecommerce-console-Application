using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using EcommerceApplication.Service;

namespace Testing.nUnitTests
{
    public class ProductCreatedTests
    {
        [SetUp]
        public void Setup()
        {
        }



        [Test]
        public void CreateproductserviceTest()
        {
            OrderProcessorService orderProcessorService = new OrderProcessorService();
           OrderProcessorRepositoryImpl orderProcessorRepository = new OrderProcessorRepositoryImpl();

            Product product = new Product()
            {
               Name = "comb",
               Price = 200,
               Description = "its just a comb",
               StockQuantity = 10               
            };


            bool addProduct = orderProcessorRepository.CreateProduct(product); 
            Assert.That(true, Is.EqualTo(addProduct));
        }


        

    }
}