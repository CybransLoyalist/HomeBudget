using System.Data.Entity;
using System.Linq;

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

        public virtual IQueryable<TModel> GetDataInFullFormat()
        {
            return GetDbSet();
        }

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
            // if (item != null)
            // {
            System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
            GetDbSet().Remove(item);
           // }
        }

        public virtual void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        [ExcludeFromCoverage]
        public virtual void SetModified(TModel yearSheet)
        {
            DbContext.Entry(yearSheet).State = EntityState.Modified;
        }

    }
}