using System.Collections.Generic;
using System.Linq;
using HomeBudget.Models;
using HomeBudget.Models.Repositories;
using System.Data.Entity;
using HomeBudgetTests.TestingHelpers;
using Moq;
using NUnit.Framework;

namespace HomeBudgetTests.Models.Repositories
{
    [TestFixture]
    class RepositoryTests
    {
        class RepositoryExample : Repository<YearSheet>
        {
            public RepositoryExample(ApplicationDbContext dbContext) : base(dbContext)
            {
            }

            protected override DbSet<YearSheet> GetDbSet()
            {
                return DbContext.YearSheets;
            }
        }

        private RepositoryExample _sut;
        private Mock<ApplicationDbContext> _applicationDbContextMock;
        private readonly DbSetMockCreator _dbSetMockCreator = new DbSetMockCreator();
        private Mock<DbSet<YearSheet>> _dbSetMock;

        [SetUp]
        public void SetUp()
        {
            var data = new List<YearSheet>
            {
                new YearSheet { Year = 2001 },
                new YearSheet { Year = 2002 },
                new YearSheet { Year = 2003 },
            }.AsQueryable();

            _dbSetMock = _dbSetMockCreator.CreateDbContextMock(data);

            _applicationDbContextMock = new Mock<ApplicationDbContext>();
            _applicationDbContextMock.Setup(c => c.YearSheets).Returns(_dbSetMock.Object);

            _sut = new RepositoryExample(_applicationDbContextMock.Object);
        }

        [Test]
        public void GetById_ShallFindItem()
        {
            var yearSheet = new YearSheet();
            _dbSetMock.Setup(a => a.Find(5)).Returns(yearSheet);

            var result = _sut.GetById(5);

            Assert.AreEqual(yearSheet, result);
            _dbSetMock.Verify(a => a.Find(5));
        }

        [Test]
        public void Adding_ShallAddItemToDbSet()
        {
            var yearSheet = new YearSheet();
            _dbSetMock.Setup(a => a.Add(yearSheet));

            _sut.Add(yearSheet);

            _dbSetMock.Verify(a => a.Add(yearSheet));
        }

        [Test]
        public void Removing_ShallRemoveItemFromDbSet()
        {
            var yearSheet = new YearSheet();
            _dbSetMock.Setup(a => a.Remove(yearSheet));

            _sut.Remove(yearSheet);

            _dbSetMock.Verify(a => a.Remove(yearSheet));
        }

        [Test]
        public void Saving_ShallSaveOnApplicationContext()
        {
            _applicationDbContextMock.Setup(a => a.SaveChanges());

            _sut.SaveChanges();

            _applicationDbContextMock.Verify(a => a.SaveChanges());
        }
    }
}
