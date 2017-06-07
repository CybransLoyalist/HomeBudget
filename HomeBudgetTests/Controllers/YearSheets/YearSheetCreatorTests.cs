using System;
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
            _sut = new YearSheetCreator(_applicationUsersRepositoryMock.Object);
        }

        [Test]
        public void NewYearSheetShallHaveYearSet_ToCurrentYear()
        {
            _applicationUsersRepositoryMock.Setup(a => a.GetById(It.IsAny<string>())).Returns(new ApplicationUser());

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
    }
}
