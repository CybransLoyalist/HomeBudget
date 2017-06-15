using System;
using System.Linq;
using System.Security.Principal;
using HomeBudget.Controllers.YearSheets;
using HomeBudget.Models;
using HomeBudget.Models.Repositories;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;

namespace HomeBudgetTests.Controllers.YearSheets
{
    [TestFixture]
    class YearSheetCreatorTests
    {
        private YearSheetCreator _sut;
        private Mock<ApplicationUsersRepository> _applicationUsersRepositoryMock;
        private Mock<IPrincipal> _principalMock;

        [SetUp]
        public void SetUp()
        {
            _principalMock = new Mock<IPrincipal>();
            _principalMock.SetupGet(a => a.Identity).Returns(new Mock<IIdentity>().Object);

            _applicationUsersRepositoryMock = new Mock<ApplicationUsersRepository>(null);
            _applicationUsersRepositoryMock.Setup(a => a.GetById(It.IsAny<string>())).Returns(new ApplicationUser());

            _sut = new YearSheetCreator(_applicationUsersRepositoryMock.Object);
        }

        [Test]
        public void NewYearSheetShallHaveYearSet_ToCurrentYear()
        {
            var result = _sut.CreateBasic(_principalMock.Object);

            Assert.AreEqual(DateTime.Now.Year, result.Year);
        }

        [Test]
        public void NewYearSheetShallHaveUserSet_ToCurrentUser()
        {
            var applicationUser = new ApplicationUser();
            _applicationUsersRepositoryMock.Setup(a => a.GetById(It.IsAny<string>())).Returns(applicationUser);

            var result = _sut.CreateBasic(_principalMock.Object);

            Assert.AreEqual(applicationUser, result.User);
        }

        [Test]
        public void NewYearSheetShallHaveUserIdSet_ToCurrentUserId()
        {
            var applicationUser = new ApplicationUser {Id = "abc"};
            _applicationUsersRepositoryMock.Setup(a => a.GetById(It.IsAny<string>())).Returns(applicationUser);

            var result = _sut.CreateBasic(_principalMock.Object);

            Assert.AreEqual(applicationUser.Id, result.User_Id);
        }

        [Test]
        public void NewYearSheet_ShallHave12MonthlySheets()
        {
            var result = _sut.CreateBasic(_principalMock.Object);

            Assert.AreEqual(12, result.Sheets.Count(a => a.Type == SheetType.Monthly));
        }

        [Test]
        public void NewYearSheet_ShallHaveOneSummarySheet()
        {
            var result = _sut.CreateBasic(_principalMock.Object);

            Assert.AreEqual(1, result.Sheets.Count(a => a.Type == SheetType.Summary));
        }

        [Test]
        public void NewYearSheet_ShallHaveOneYearSpanSheet()
        {
            var result = _sut.CreateBasic(_principalMock.Object);

            Assert.AreEqual(1, result.Sheets.Count(a => a.Type == SheetType.YearSpan));
        }
    }
}
