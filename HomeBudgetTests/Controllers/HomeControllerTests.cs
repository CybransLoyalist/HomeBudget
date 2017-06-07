using System.Web.Mvc;
using HomeBudget.Controllers;
using HomeBudget.Models;
using HomeBudget.Models.Repositories;
using Moq;
using NUnit.Framework;

namespace HomeBudgetTests.Controllers
{
    [TestFixture]
    class HomeControllerTests
    {
        private HomeController _sut;
        private Mock<YearSheetsRepository> _yearSheetRepositoryMock;
        private Mock<ControllerContext> _controllerContextMock;

        [SetUp]
        public void SetUp()
        {
            _yearSheetRepositoryMock = new Mock<YearSheetsRepository>(null);
            _sut = new HomeController(_yearSheetRepositoryMock.Object);

            _controllerContextMock = new Mock<ControllerContext>();
            _sut.ControllerContext = _controllerContextMock.Object;
        }

        [Test]
        public void IfUserNotAuthenticated_ShallRedirectTo_Login()
        {
            _controllerContextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(false);

            var result = _sut.Index();

            Assert.IsInstanceOf<RedirectToRouteResult>(result);
            Assert.AreEqual("Account", (result as RedirectToRouteResult).RouteValues["controller"]);
            Assert.AreEqual("Login", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [Test]
        public void IfThereIsNoYearSheetForCurrentUser_ShallRedirectTo_NoYearSheetPresent()
        {
            _controllerContextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            _yearSheetRepositoryMock.Setup(a => a.GetForUser(It.IsAny<string>())).Returns((YearSheet)null);

            var result = _sut.Index();

            Assert.IsInstanceOf<RedirectToRouteResult>(result);
            Assert.AreEqual("YearSheet", (result as RedirectToRouteResult).RouteValues["controller"]);
            Assert.AreEqual("NoYearSheetPresent", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [Test]
        public void IfThereIsYearSheetForCurrentUser_ShallRedirectTo_CurrentYearSheet()
        {
            _controllerContextMock.SetupGet(p => p.HttpContext.User.Identity.IsAuthenticated).Returns(true);
            _yearSheetRepositoryMock.Setup(a => a.GetForUser(It.IsAny<string>())).Returns(new YearSheet());

            var result = _sut.Index();

            Assert.IsInstanceOf<RedirectToRouteResult>(result);
            Assert.AreEqual("YearSheet", (result as RedirectToRouteResult).RouteValues["controller"]);
            Assert.AreEqual("Current", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [Test]
        public void About_ShallGet_AboutView()
        {
            var result = _sut.About();
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.NotNull(result);
        }

        [Test]
        public void Contact_ShallGet_AboutView()
        {
            var result = _sut.Contact();
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.NotNull(result);
        }
    }
}
