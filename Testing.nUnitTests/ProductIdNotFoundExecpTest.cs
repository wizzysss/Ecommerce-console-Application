using EcommerceApplication.Models;
using EcommerceApplication.Repository;
using EcommerceApplication.Service;
using EcommerceApplication.Exceptions;

namespace Testing.nUnitTests
{
    public class ProductIdNotFoundExceptionTest
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]

        [TestCase(43, ExpectedResult = 0)]
        [TestCase(45, ExpectedResult = 0)]
        public int ProductIdNotFoundExecptionTest(int ProductId)
        {
            OrderProcessorService orderProcessorService = new OrderProcessorService();
            OrderProcessorRepositoryImpl orderProcessorRepository = new OrderProcessorRepositoryImpl();


            bool Pnotfound = orderProcessorRepository.ProductNotExist(ProductId);
            return Pnotfound ? 1 : 0;
        }

    }
}