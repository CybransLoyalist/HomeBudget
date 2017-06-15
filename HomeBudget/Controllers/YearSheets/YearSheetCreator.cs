using System;
using System.Collections.Generic;
using System.Globalization;
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

            var yearSheet = new YearSheet
            {
                User = user,
                User_Id = user.Id,
                Year = DateTime.Now.Year,
                Sheets = new List<Sheet>()
            };

            yearSheet.Sheets.Add(CreateSummarySheet());
            yearSheet.Sheets.Add(CreateHolidaySheet());
            yearSheet.Sheets.AddRange(CreateMonthlySheets());

            return yearSheet;
        }

        private IEnumerable<Sheet> CreateMonthlySheets()
        {
            var result = new List<Sheet>();

            for (int i = 1; i < 13; ++i)
            {
                result.Add(new Sheet {Type = SheetType.Monthly, Title = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)});
            }

            return result;
        }

        private Sheet CreateSummarySheet()
        {
            return new Sheet {Type = SheetType.Summary, Title = "Summary"};
        }

        private Sheet CreateHolidaySheet()
        {
            return new Sheet {Type = SheetType.YearSpan, Title = "Holidays"};
        }
    }
}