using System.Web.Mvc;
using HomeBudget.Models.Repositories;
using Microsoft.AspNet.Identity;

namespace HomeBudget.Controllers
{
    public class YearSheetController : Controller
    {
        private readonly YearSheetRepository _yearSheetRepository;

        public YearSheetController(YearSheetRepository yearSheetRepository)
        {
            _yearSheetRepository = yearSheetRepository;
        }

        public ActionResult NoYearSheetPresent()
        {
            return View();
        }

        public ActionResult Current()
        {
            var yearSheet = _yearSheetRepository.GetForUser(User.Identity.GetUserId());
            return View(yearSheet);
        }
    }
}