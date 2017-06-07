using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HomeBudget.Models;
using HomeBudget.Models.Repositories;
using HomeBudgetTests.TestingHelpers;
using Moq;
using NUnit.Framework;

namespace HomeBudgetTests.Models.Repositories
{
    [TestFixture]
    class YearSheetsRepositoryTests
    {
        private YearSheetsRepository _sut;
        private Mock<ApplicationDbContext> _applicationDbContextMock;
        private readonly DbSetMockCreator _dbSetMockCreator = new DbSetMockCreator();
        private Mock<DbSet<YearSheet>> _dbSetMock;

        [SetUp]
        public void SetUp()
        {
            var data = new List<YearSheet>
            {
                new YearSheet { Year = 2001, User = new ApplicationUser {Id = "1"}},
                new YearSheet { Year = 1000, User = new ApplicationUser {Id = "3"}},
                new YearSheet { Year = 2003, User = new ApplicationUser {Id = "2"}},
                new YearSheet { Year = 2000, User = new ApplicationUser {Id = "3"}},
            }.AsQueryable();

            _dbSetMock = _dbSetMockCreator.CreateDbContextMock(data);

            _applicationDbContextMock = new Mock<ApplicationDbContext>();
            _applicationDbContextMock.Setup(c => c.YearSheets).Returns(_dbSetMock.Object);

            _sut = new YearSheetsRepository(_applicationDbContextMock.Object);
        }

        [Test]
        public void GetForUser_ShallGetFirstYearSheetForGivenUser()
        {
            var result = _sut.GetForUser("3");

            Assert.AreEqual(1000, result.Year);
        }
    }
}