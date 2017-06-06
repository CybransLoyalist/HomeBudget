using System.Data.Entity;
using System.Linq;

namespace HomeBudget.Models.Repositories
{
    public class YearSheetRepository : Repository<YearSheet>
    {
        public YearSheetRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<YearSheet> GetDbSet()
        {
            return DbContext.YearSheets;
        }

        public virtual YearSheet GetForUser(string userId)
        {
            return GetDbSet().FirstOrDefault(a => a.User.Id == userId);
        }
    }
}