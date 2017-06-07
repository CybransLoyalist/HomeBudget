using System.Security.Principal;
using System.Web.Mvc;
using HomeBudget.Controllers.YearSheets;
using HomeBudget.Models;
using HomeBudget.Models.Repositories;
using Moq;
using NUnit.Framework;

namespace HomeBudgetTests.Controllers.YearSheets
{
    [TestFixture]
    class YearSheetControllerTests
    {
        private YearSheetController _sut;
        private Mock<YearSheetsRepository> _yearSheetsRepositoryMock;
        private Mock<YearSheetCreator> _yearSheetCreatorMock;
        private Mock<ControllerContext> _controllerContextMock;

        [SetUp]
        public void SetUp()
        {
            _yearSheetsRepositoryMock = new Mock<YearSheetsRepository>(null);
            _yearSheetCreatorMock = new Mock<YearSheetCreator>(null);
            _sut = new YearSheetController(_yearSheetsRepositoryMock.Object, _yearSheetCreatorMock.Object);

            _controllerContextMock = new Mock<ControllerContext>();
            _controllerContextMock.SetupGet(p => p.HttpContext.User.Identity).Returns(new Mock<IIdentity>().Object);
            _sut.ControllerContext = _controllerContextMock.Object;
        }

        [Test]
        public void NoYearSheetPresent_ShallCreateView()
        {
            var result = _sut.NoYearSheetPresent();
            Assert.NotNull(result);
        }

        [Test]
        public void Current_ShallGetCurrentYearSheetForUser_AndGetViewWithThisSheetAsModel()
        {
            var yearSheet = new YearSheet();
            _yearSheetsRepositoryMock.Setup(a => a.GetForUser(It.IsAny<string>())).Returns(yearSheet);

            var result = _sut.Current();

            _yearSheetsRepositoryMock.Verify(a => a.GetForUser(It.IsAny<string>()));
            Assert.AreEqual(yearSheet, (result as ViewResult).Model);
        }

        [Test]
        public void GetNewYearSheetCreatingPage_ShallCreateBasicYearSheet_AndGetCreateViewWithThisSheetAsModel()
        {
            var yearSheet = new YearSheet();
            _yearSheetCreatorMock.Setup(a => a.CreateBasic(It.IsAny<IPrincipal>())).Returns(yearSheet);

            var result = _sut.GetNewYearSheetCreatingPage();

            _yearSheetCreatorMock.Verify(a => a.CreateBasic(It.IsAny<IPrincipal>()));
            Assert.AreEqual(yearSheet, (result as ViewResult).Model);
            Assert.AreEqual("Create", (result as ViewResult).ViewName);
        }
    }
}
