using System.Data.Entity;
using System.Linq;
using Moq;

namespace HomeBudgetTests.TestingHelpers
{
    class DbSetMockCreator
    {
        public Mock<DbSet<TModel>> CreateDbContextMock<TModel>(IQueryable<TModel> data) where TModel : class
        {
            var mockSet = new Mock<DbSet<TModel>>();
            mockSet.As<IQueryable<TModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet;
        }
    }
}