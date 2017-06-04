using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;

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

        public YearSheet GetForUser(IPrincipal user)
        {
            var userId = user.Identity.GetUserId();
            return GetDbSet().FirstOrDefault(a => a.User.Id == userId);
        }
    }
}