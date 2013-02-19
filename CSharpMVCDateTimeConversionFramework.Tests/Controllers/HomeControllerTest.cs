using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpMVCDateTimeConversionFramework.Controllers;

namespace CSharpMVCDateTimeConversionFramework.Tests.Controllers {
    [TestClass]
    public class HomeControllerTest {
        [TestMethod]
        public void Index() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Custom Model Binder for Date Time Conversion Test MVC Application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void About() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.UiDateTimeModel() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.UiDateTimeModelBinder() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
