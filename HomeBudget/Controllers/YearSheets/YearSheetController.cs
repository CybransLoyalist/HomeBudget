using System;
using System.Web.Mvc;
using HomeBudget.Models;
using HomeBudget.Models.Repositories;
using Microsoft.AspNet.Identity;

namespace HomeBudget.Controllers.YearSheets
{
    public class YearSheetController : Controller
    {
        private readonly YearSheetsRepository _yearSheetsRepository;
        private readonly SheetsRepository _sheetsRepository;
        private readonly YearSheetCreator _yearSheetCreator;

        public YearSheetController(
            YearSheetsRepository yearSheetsRepository,
            SheetsRepository sheetsRepository,
            YearSheetCreator yearSheetCreator)
        {
            _yearSheetsRepository = yearSheetsRepository;
            _sheetsRepository = sheetsRepository;
            _yearSheetCreator = yearSheetCreator;
        }

        public ActionResult NoYearSheetPresent()
        {
            return View();
        }

        public ActionResult Current()
        {
            var yearSheet = _yearSheetsRepository.GetForUser(User.Identity.GetUserId());
            return View("YearSheetView", yearSheet);
        }

        public ActionResult ViewYearSheet(int id)
        {
            var yearSheet = _yearSheetsRepository.GetById(id);
            return View("YearSheetView", yearSheet);
        }

        public ActionResult GetNewYearSheetCreatingPage()
        {
            var yearSheet = _yearSheetCreator.CreateBasic(User);
            _yearSheetsRepository.Add(yearSheet);
            _yearSheetsRepository.SaveChanges();

            return View("Edit", yearSheet);
        }

        [HttpPost]
        public ActionResult Save(YearSheet yearSheet)
        {
            if (ModelState.IsValid)
            {
                _yearSheetsRepository.SetModified(yearSheet);
                _yearSheetsRepository.SaveChanges();
                return ShowList();
            }
            return View("Edit", yearSheet);
        }

        public ActionResult ShowList()
        {
            var yearSheets = _yearSheetsRepository.GetAllForUser(User.Identity.GetUserId());
            return View("Index", yearSheets);
        }

        public ActionResult Edit(int id)
        {
            var yearSheet = _yearSheetsRepository.GetById(id);
            return View("Edit", yearSheet);
        }

        public ActionResult Delete(int id)
        {
            var sheetsToBeDeleted = _sheetsRepository.GetAllForYearSheet(id);
            foreach (var sheetToBeDeleted in sheetsToBeDeleted)
            {
                _sheetsRepository.Remove(sheetToBeDeleted);
            }
            _sheetsRepository.SaveChanges();

            var yearSheetToBeDeleted = _yearSheetsRepository.GetById(id);
            _yearSheetsRepository.Remove(yearSheetToBeDeleted);
            _yearSheetsRepository.SaveChanges();

            var yearSheets = _yearSheetsRepository.GetAllForUser(User.Identity.GetUserId());
            return View("Index", yearSheets);
        }
    }
}