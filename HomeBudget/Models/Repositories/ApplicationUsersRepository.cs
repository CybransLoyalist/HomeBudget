using System.Data.Entity;
using System.Linq;

namespace HomeBudget.Models.Repositories
{
    [ExcludeFromCoverage]
    public class ApplicationUsersRepository : Repository<ApplicationUser>
    {
        public ApplicationUsersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<ApplicationUser> GetDbSet()
        {
            return DbContext.Users as DbSet<ApplicationUser>;
        }

        public virtual ApplicationUser GetById(string userId)
        {
            return GetDbSet().FirstOrDefault(a => a.Id == userId);
        }
    }
}