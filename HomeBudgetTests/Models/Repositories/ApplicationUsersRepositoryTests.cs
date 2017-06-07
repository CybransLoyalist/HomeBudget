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
    class ApplicationUsersRepositoryTests
    {
        private ApplicationUsersRepository _sut;
        private Mock<ApplicationDbContext> _applicationDbContextMock;
        private readonly DbSetMockCreator _dbSetMockCreator = new DbSetMockCreator();
        private Mock<DbSet<ApplicationUser>> _dbSetMock;
        private IQueryable<ApplicationUser> _data;

        [SetUp]
        public void SetUp()
        {
            _data = new List<ApplicationUser>
            {
                new ApplicationUser {Id = "123"},
                new ApplicationUser {Id = "345"},
                new ApplicationUser {Id = "456"},
            }.AsQueryable();

            _dbSetMock = _dbSetMockCreator.CreateDbContextMock(_data);

            _applicationDbContextMock = new Mock<ApplicationDbContext>();
            _applicationDbContextMock.Setup(c => c.Users).Returns(_dbSetMock.Object);

            _sut = new ApplicationUsersRepository(_applicationDbContextMock.Object);
        }

        [Test]
        public void ShallGetByUserId()
        {
            var result = _sut.GetById("123");

            Assert.AreEqual(_data.First(), result);
        }
    }
}