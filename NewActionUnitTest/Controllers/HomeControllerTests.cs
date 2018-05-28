using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NewAuction.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            HomeController homeController = new HomeController();
            ViewResult vr = homeController.Index() as ViewResult;
            Assert.Fail();
        }
    }
}