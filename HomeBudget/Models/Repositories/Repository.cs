using System.Data.Entity;

namespace HomeBudget.Models.Repositories
{
    public abstract class Repository<TModel> where TModel : class
    {
        protected ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected abstract DbSet<TModel> GetDbSet();

        public TModel GetById(int id)
        {
            return GetDbSet().Find(id);
        }

        public void Add(TModel item)
        {
            GetDbSet().Add(item);
        }

        public void Remove(TModel item)
        {
            GetDbSet().Remove(item);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

    }
}