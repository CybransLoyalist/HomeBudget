using System;
using System.Security.Principal;
using HomeBudget.Models;
using HomeBudget.Models.Repositories;
using Microsoft.AspNet.Identity;

namespace HomeBudget.Controllers.YearSheets
{
    public class YearSheetCreator
    {
        private readonly ApplicationUsersRepository _applicationUsersRepository;

        public YearSheetCreator(ApplicationUsersRepository applicationUsersRepository)
        {
            _applicationUsersRepository = applicationUsersRepository;
        }

        public virtual YearSheet CreateBasic(IPrincipal principal)
        {
            var user = _applicationUsersRepository.GetById(principal.Identity.GetUserId());
            var yearSheet = new YearSheet { User = user, Year = DateTime.Now.Year };
            return yearSheet;
        }
    }
}