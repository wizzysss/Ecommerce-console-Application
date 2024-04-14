using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using EcommerceApplication.Service;

namespace Testing.nUnitTests
{
    public class ProductTest
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
                Name = "Hair Band",
                Price = 20,
                Description = "Men's hair band",
                StockQuantity = 20
            };


            bool addProduct = orderProcessorRepository.CreateProduct(product);
            Assert.That(true, Is.EqualTo(addProduct));
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


        [Test]

        [TestCase(43, ExpectedResult = 0)]
        [TestCase(3, ExpectedResult = 1)]
        public int ProductIdNotFoundExecptionTest(int ProductId)
        {
            OrderProcessorService orderProcessorService = new OrderProcessorService();
            OrderProcessorRepositoryImpl orderProcessorRepository = new OrderProcessorRepositoryImpl();


            bool Pnotfound = orderProcessorRepository.ProductNotExist(ProductId);
            return Pnotfound ? 1 : 0;
        }

        [Test]
        public void ProductInOrder()
        {

        }
        //{

        //    OrderProcessorService orderProcessorService = new OrderProcessorService();

        //    Customer customer = new Customer()
        //    {
        //        CustomerId = 1,
        //    };

        //    Product product = new Product()
        //    {
        //        ProductId = 2,
        //    };

        //    OrderItem orderItem = new OrderItem()
        //    {
        //        Quantity = 3,
        //    };
        //    Order order = new Order()
        //    {
        //        ShippingAddress = "avadi",
        //    };



        //bool ProductedAdded = orderProcessorService.Placeorderservice(customer, order, orderItem, product);


        //Assert.That(true, Is.EqualTo(ProductedAdded));
        //}
    }
}