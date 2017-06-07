using System;
using System.Web.Mvc;
using HomeBudget.Models.Repositories;
using Microsoft.AspNet.Identity;

namespace HomeBudget.Controllers.YearSheets
{
    public class YearSheetController : Controller
    {
        private readonly YearSheetsRepository _yearSheetsRepository;
        private readonly YearSheetCreator _yearSheetCreator;

        public YearSheetController(
            YearSheetsRepository yearSheetsRepository,
            YearSheetCreator yearSheetCreator)
        {
            _yearSheetsRepository = yearSheetsRepository;
            _yearSheetCreator = yearSheetCreator;
        }

        public ActionResult NoYearSheetPresent()
        {
            return View();
        }

        public ActionResult Current()
        {
            var yearSheet = _yearSheetsRepository.GetForUser(User.Identity.GetUserId());
            return View(yearSheet);
        }

        public ActionResult GetNewYearSheetCreatingPage()
        {
            var yearSheet = _yearSheetCreator.CreateBasic(User);

            return View("Create", yearSheet);
        }

        [HttpPost]
        public ActionResult CreateNew()
        {
            throw new NotImplementedException();
        }
    }
}