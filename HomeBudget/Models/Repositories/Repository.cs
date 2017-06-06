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

        public virtual TModel GetById(int id)
        {
            return GetDbSet().Find(id);
        }

        public virtual void Add(TModel item)
        {
            GetDbSet().Add(item);
        }

        public virtual void Remove(TModel item)
        {
            GetDbSet().Remove(item);
        }

        public virtual void SaveChanges()
        {
            DbContext.SaveChanges();
        }

    }
}