using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HomeBudget.Models.Repositories
{
    public class YearSheetsRepository : Repository<YearSheet>
    {
        public YearSheetsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<YearSheet> GetDbSet()
        {
            return DbContext.YearSheets;
        }

        protected override IQueryable<YearSheet> GetDataInFullFormat()
        {
            return GetDbSet().Include(a => a.User).Include(a => a.Sheets);
        }

        public override YearSheet GetById(int id)
        {
            return GetDataInFullFormat().FirstOrDefault(a => a.Id == id);
        }

        public virtual YearSheet GetForUser(string userId)
        {
            return GetDataInFullFormat().FirstOrDefault(a => a.User.Id == userId);
        }

        public virtual IEnumerable<YearSheet> GetAllForUser(string userId)
        {
            return GetDataInFullFormat().Where(a => a.User_Id == userId);
        }
    }
}